using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet <Dish> Dishes { get; set; }
    public DbSet <Order> Orders { get; set; }
    public DbSet<DishInCart> DishesInCart { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<ActiveToken> ActiveTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Rating>().HasKey(r => r.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}