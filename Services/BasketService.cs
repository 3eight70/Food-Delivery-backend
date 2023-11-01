using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class BasketService : IBasketService
{
    private readonly AppDbContext _context;

    public BasketService(AppDbContext context)
    {
        _context = context;
    }

    public DishBasketDTO[] GetCart(string token)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(id => token == id.token);

        var dishes = _context.DishesInCart.Where(cart => cart.userId == userToken.userId).ToList();
        List<DishBasketDTO> dishesInCart = new List<DishBasketDTO>();
        
        foreach (DishInCart dish in dishes)
        {
            var currentDish = _context.Dishes.FirstOrDefault(dsh => dsh.Id == dish.dishId);
            dishesInCart.Add(new DishBasketDTO
            {
                Id = Guid.NewGuid(),
                Amount = dish.Amount,
                dishId = dish.dishId,
                Image = currentDish.Photo,
                Name = currentDish.Name,
                Price = currentDish.Price,
                TotalPrice = dish.Amount * currentDish.Price
            });
        }

        return dishesInCart.ToArray();
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
        DishInCart? dishInCart = _context.DishesInCart.FirstOrDefault(dsh => dsh.dishId == dishId);
        if (dishInCart != null)
        {
            dishInCart.Amount += 1;
        }
        else
        {
            await _context.DishesInCart.AddAsync(new DishInCart
            {
                Id = new Guid(),
                Amount = 1,
                dishId = dish.Id,
                userId = user.Id
            });
        }

        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "Dish successfully added"
        });
    }

    public async Task<ActionResult> DecreaseDish(string token, Guid dishId, bool increase = false)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);
        Dish? dish = _context.Dishes.FirstOrDefault(dsh => dsh.Id == dishId);

        if (dish == null)
        {
            throw new InvalidOperationException("Invalid dishId");
        }

        DishInCart? dishInCart =
            _context.DishesInCart.FirstOrDefault(dsh => dsh.dishId == dish.Id && dsh.userId == userToken.userId);
        if (dishInCart == null)
        {
            throw new InvalidOperationException("User doesn't have this dish in his cart");
        }

        if (!increase || dishInCart.Amount == 1)
        {
            _context.DishesInCart.Remove(dishInCart);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                message = "Dish successfully deleted"
            });
        }

        dishInCart.Amount -= 1;
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "Amount of dishes successfully decreased"
        });
    }
}