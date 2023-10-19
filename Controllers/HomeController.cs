using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

public class HomeController : Controller
{
    private AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;

        if (_context.Dishes.Count() == 0)
        {
            Dish bananaAndAr = new Dish(new Guid(), "Сладкий ролл с арахисом и бананом", 210,
                "Сырная лепешка, банан, арахис, сливочный сыр, шоколадная крошка, топинг карамельный", "https://mistertako.ru/uploads/products/a4772f7a-7a6f-11eb-850a-0050569dbef0.jpeg", true,
                Category.Dessert);
            
            Dish bananaAndKiwi = new Dish (new Guid(), "Сладкий ролл с бананом и киви", 220,
                "Сырная лепешка, банан, киви, сливочный сыр, топинг клубничный", "https://mistertako.ru/uploads/products/9e7c8581-7a6f-11eb-850a-0050569dbef0.jpeg", true,
                Category.Dessert);

            Dish ramen = new Dish(new Guid(), "Сливочный рамен с курицей и шампиньонами", 260,
                "Бульон рамен со сливками (куриный бульон, чесночно-имбирная заправка, соевый соус) с пшеничной лапшой, отварной курицей, омлетом Томаго и шампиньонами. БЖУ на 100 г. Белки, г — 8,13 Жиры, г — 6,18 Углеводы, г — 8,08",
                "https://mistertako.ru/uploads/products/ccd8e2de-5f36-11e8-8f7d-00155dd9fd01.jpg", false,
                Category.Soup);

            Dish wokVegetables = new Dish(new Guid(), "Wok том ям с овощами", 250,
                "Лапша пшеничная, шампиньоны, лук красный, заправка Том Ям (паста Том Ям, паста Том Кха, сахар, соевый соус), сливки, соевый соус, помидор, перец чили. БЖУ на 100 г. Белки, г - 5,32 Жиры, г - 14,89 Углеводы, г - 22,46",
                "https://mistertako.ru/uploads/products/cd661716-54ed-11ed-8575-0050569dbef0.jpg", true, Category.WOK);

            Dish belissimo = new Dish(new Guid(), "Белиссимо", 400,
                "Копченая куриная грудка, свежие шампиньоны, маринованные опята, сыр «Моцарелла», сыр «Гауда», сливочно-чесночный соус, свежая зелень.",
                "https://mistertako.ru/uploads/products/9ee8ed49-8327-11ec-8575-0050569dbef0.jpg", false,
                Category.Pizza);

            Dish classicCocktail = new Dish(new Guid(), "Коктейль классический", 140, "Классический молочный коктейль",
                "https://mistertako.ru/uploads/products/120b46bc-5f32-11e8-8f7d-00155dd9fd01.jpg", true,
                Category.Drink);
            
            _context.Dishes.AddRange(bananaAndAr, bananaAndKiwi, ramen, wokVegetables, belissimo, classicCocktail);
            _context.SaveChanges();
        }
    }

    public async Task<DishPagedListDTO> Index(int page = 1)
    {
        int pageSize = 5;

        IQueryable<Dish> source = _context.Dishes;
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        PageInfo pageInfo = new PageInfo(count, page, pageSize);
        DishPagedListDTO dishPagedList= new DishPagedListDTO
        {
            PageInfo = pageInfo,
            Dishes = items
        };

        return dishPagedList;     
    }
}