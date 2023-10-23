using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class BasketService: IBasketService
{
    private readonly AppDbContext _context;

    public BasketService(AppDbContext context)
    {
        _context = context;
    }

    public DishBasketDTO[] GetCart(string token)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(id => token == id.token);

        var dishes = (from user in _context.Users
                where user.Id == userToken.userId
                from DishesInCart in _context.DishesInCart.Where(dish => dish.userId == user.Id)
                from Dishes in _context.Dishes.Where(dish => DishesInCart.dishId == dish.Id)
                where (user != null && DishesInCart != null)
                group Dishes by Dishes.Id into dishGroup
                select new DishBasketDTO
                {
                    Id = dishGroup.First().Id,
                    Name = dishGroup.First().Name,
                    Price = dishGroup.First().Price,
                    TotalPrice = dishGroup.Sum(dish => dish.Price * _context.DishesInCart.Count(dsh => dsh.dishId == dish.Id)),
                    Amount = _context.DishesInCart.Count(dish => dish.Id == dishGroup.Key),
                    Image = dishGroup.First().Photo
                }
            ).AsEnumerable().ToArray();
        
        return dishes;
    }

    public async Task<ActionResult> AddDish(string token, Guid dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(dsh => dsh.Id == dishId);

        if (dish == null)
        {
            throw new InvalidOperationException("Invalid dishId");
        }

        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);
        User user = _context.Users.FirstOrDefault(usr => usr.Id == userToken.userId);

        await _context.DishesInCart.AddAsync(new DishInCart
        {
            Id = new Guid(),
            Amount = 1,
            dishId = dish.Id,
            userId = user.Id
        });

        await _context.SaveChangesAsync();
        return new OkObjectResult(new
        {
            message = "Dish successfully added"
        });
    }
}