﻿using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet <Dish> Dishes { get; set; }
    public DbSet <Order> Orders { get; set; }
    public DbSet<DishInCart> DishesInCart { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<ActiveToken> ActiveTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(options =>
        {
            options.HasIndex(x => x.FullName);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}