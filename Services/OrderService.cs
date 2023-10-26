using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    private readonly GarContext _garContext;

    private readonly IBasketService _basketService;

    public OrderService(AppDbContext context, GarContext garContext, IBasketService basketService)
    {
        _context = context;
        _garContext = garContext;
        _basketService = basketService;
    }

    public OrderInfoDTO[] GetList(string token)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        List<Order> orders = _context.Orders.Where(order => order.userId == userToken.userId).ToList();

        List<OrderInfoDTO> orderDTOs = new List<OrderInfoDTO>();

        foreach (Order order in orders)
        {
            orderDTOs.Add(new OrderInfoDTO
            {
                Id = order.Id,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                status = order.status,
                Price = order.Price,
            });
        }

        return orderDTOs.ToArray();
    }

    public async Task<ActionResult> CreateOrder(OrderCreateDTO order, string token)
    {
        if (order.DeliveryTime < DateTime.UtcNow.AddMinutes(60))
        {
            throw new InvalidOperationException(
                "Invalid delivery time. Delivery time must be more than current datetime on 60 minutes");
        }

        var address = _garContext.AsAddrObjs.FirstOrDefault(add => add.Objectguid == order.AddressId);

        if (address != null)
        {
            throw new InvalidOperationException("AddressId isn't identifier of building");
        }

        var house = _garContext.AsHouses.FirstOrDefault(h => h.Objectguid == order.AddressId);

        if (house == null)
        {
            throw new InvalidDataException("Not found object with ObjectGuid=" + order.AddressId);
        }

        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        if (userToken == null) return null;

        var dishesInCart = _basketService.GetCart(token).ToList();

        if (dishesInCart.IsNullOrEmpty())
        {
            throw new InvalidOperationException("User doesn't have dishes in his cart");
        }


        double totalPrice = 0;

        foreach (DishBasketDTO dish in dishesInCart)
        {
            totalPrice += dish.TotalPrice;
        }

        await _context.Orders.AddAsync(new Order
        {
            Id = new Guid(),
            DeliveryTime = order.DeliveryTime,
            OrderTime = DateTime.UtcNow,
            status = Status.InProcess,
            Price = totalPrice,
            DishesInCart = dishesInCart,
            userId = userToken.userId
        });

        var dishes = _context.DishesInCart.Where(usr => usr.userId == userToken.userId).ToList();

        foreach (DishInCart dish in dishes)
        {
            _context.DishesInCart.Remove(dish);
        }

        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "Order successfully created"
        });
    }

    public OrderDTO GetInfo(string token, Guid id)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => token == tkn.token);

        Order? order = _context.Orders.Include(ord => ord.DishesInCart)
            .FirstOrDefault(ord => ord.Id == id && ord.userId == userToken.userId);

        if (order == null)
        {
            throw new InvalidOperationException("Order with current id doesn't exist");
        }

        return new OrderDTO
        {
            DeliveryTime = order.DeliveryTime,
            OrderTime = order.OrderTime,
            status = order.status,
            Price = order.Price,
            DishesInCart = order.DishesInCart
        };
    }

    public async Task<ActionResult> ConfirmOrder(string token, Guid id)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        Order? order = _context.Orders.FirstOrDefault(ord => ord.Id == id && ord.userId == userToken.userId);

        if (order == null)
        {
            throw new InvalidOperationException("Order with this id doesn't exist");
        }

        order.status = Status.Delivered;

        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "Order confirmed"
        });
    }
}