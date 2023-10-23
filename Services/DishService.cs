using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class DishService: IDishService
{
    private readonly AppDbContext _context;

    public DishService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<DishPagedListDTO> GetDishes(Category[] categories, DishSorting sorting, bool vegetarian, int page)
    {
        var dishes = _context.Dishes.AsQueryable();

        if (categories != null && categories.Any())
        {
            dishes = dishes.Where(dish => categories.Contains(dish.Category));
        }

        dishes = dishes.Where(dish => dish.IsVegetarian == vegetarian);

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

        var dishList = await dishes.ToListAsync();

        PageInfo pageInfo = new PageInfo(dishList.Count(), page, 5);
        DishPagedListDTO response = new DishPagedListDTO(dishList, pageInfo);
        
        return response;
    }

    public DishDTO GetDish(Guid id)
    {
        var dish = _context.Dishes.FirstOrDefault(dish => dish.Id == id);

        if (dish == null)
        {
            throw new InvalidOperationException(id.ToString());
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
}