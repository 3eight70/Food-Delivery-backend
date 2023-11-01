using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class DishService : IDishService
{
    private readonly AppDbContext _context;

    public DishService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DishPagedListDTO?> GetDishes(Category[] categories, DishSorting sorting, bool vegetarian, int page)
    {
        var dishes = _context.Dishes.AsQueryable();

        if (categories != null && categories.Any())
        {
            dishes = dishes.Where(dish => categories.Contains(dish.Category));
        }

        if (vegetarian == true)
        {
            dishes = dishes.Where(dish => dish.IsVegetarian == vegetarian);
        }

        if (dishes.IsNullOrEmpty())
        {
            throw new InvalidOperationException("There aren't dishes with such attributes");
        }

        if (sorting == DishSorting.NameAsc)
        {
            dishes = dishes.OrderBy(dish => dish.Name);
        }
        else if (sorting == DishSorting.NameDesc)
        {
            dishes = dishes.OrderByDescending(dish => dish.Name);
        }
        else if (sorting == DishSorting.PriceAsc)
        {
            dishes = dishes.OrderBy(dish => dish.Price);
        }
        else if (sorting == DishSorting.PriceDesc)
        {
            dishes = dishes.OrderByDescending(dish => dish.Price);
        }
        else if (sorting == DishSorting.RatingAsc)
        {
            dishes = dishes.OrderBy(dish => dish.Rating);
        }
        else if (sorting == DishSorting.RatingDesc)
        {
            dishes = dishes.OrderByDescending(dish => dish.Rating);
        }

        const int pageSize = 5;

        int amountOfDishList = dishes.Count();
        var dishList = await dishes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        PageInfo pageInfo = new PageInfo(amountOfDishList, page, pageSize);

        if (pageInfo.TotalPages < page)
        {
            throw new InvalidOperationException("Invalid page number");
        }

        DishPagedListDTO response = new DishPagedListDTO(dishList, pageInfo);

        return response;
    }

    public DishDTO GetDish(Guid id)
    {
        var dish = _context.Dishes.FirstOrDefault(dish => dish.Id == id);

        if (dish == null)
        {
            throw new InvalidOperationException($"Dish with id={id.ToString()} doesn't exist in database");
        }

        DishDTO _dish = new DishDTO
        {
            Category = dish.Category,
            IsVegetarian = dish.IsVegetarian,
            Description = dish.Description,
            Name = dish.Name,
            Photo = dish.Photo,
            Price = dish.Price,
            Rating = dish.Rating,
        };

        return _dish;
    }

    public bool CheckIfUserCanRateDish(string token, Guid dishId)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        List<Order> orders = _context.Orders.Include(ord => ord.DishesInCart)
            .Where(ord => ord.userId == userToken.userId && ord.status == Status.Delivered).ToList();

        Dish? dish = _context.Dishes.FirstOrDefault(dsh => dsh.Id == dishId);

        if (dish == null)
        {
            throw new InvalidOperationException("Dish with this id doesn't exist");
        }

        foreach (Order order in orders)
        {
            foreach (DishBasketDTO d in order.DishesInCart)
            {
                if (d.dishId == dish.Id)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public async Task<ActionResult> SetRating(string token, Guid id, Int32 ratingScore)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        Dish? dish = _context.Dishes.FirstOrDefault(dsh => dsh.Id == id);

        if (dish == null)
        {
            throw new InvalidOperationException("Dish with this id doesn't exist");
        }

        if (!CheckIfUserCanRateDish(token, id))
        {
            throw new InvalidOperationException("User can't set rating on dish that wasn't ordered");
        }

        Rating? rating =
            _context.Ratings.FirstOrDefault(rat => rat.UserId == userToken.userId && rat.DishId == dish.Id);

        if (rating == null)
        {
            rating = new Rating(new Guid(), userToken.userId, dish.Id, ratingScore);
            await _context.Ratings.AddAsync(rating);
        }
        else
        {
            rating.Value = ratingScore;
        }

        await _context.SaveChangesAsync();
        dish.Rating = _context.Ratings.Where(rating => rating.DishId == dish.Id).Sum(rating => rating.Value) /
                      _context.Ratings.Count(rating => rating.DishId == dish.Id);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "Rating successfully added"
        });
    }
}