﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webNET_Hits_backend_aspnet_project_1.Data;

#nullable disable

namespace webNET_Hits_backend_aspnet_project_1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.ActiveToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("userId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ActiveTokens");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.AddressElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("ObjectId")
                        .HasColumnType("integer");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AddressElements");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.DishInCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid>("Dish")
                        .HasColumnType("uuid");

                    b.Property<Guid>("User")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Dish");

                    b.HasIndex("User");

                    b.ToTable("DishesInCart");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Hierarchy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("ObjectId")
                        .HasColumnType("integer");

                    b.Property<int>("ParentObjId")
                        .HasColumnType("integer");

                    b.Property<Guid>("addressElementId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("houseId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("addressElementId");

                    b.HasIndex("houseId");

                    b.ToTable("Hierarchies");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.House", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddNum1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddNum2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddType1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddType2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("ObjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DishId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Address")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("gender")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FullName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.DishInCart", b =>
                {
                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.Dish", "dish")
                        .WithMany()
                        .HasForeignKey("Dish")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dish");

                    b.Navigation("user");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Hierarchy", b =>
                {
                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.AddressElement", "addressElement")
                        .WithMany()
                        .HasForeignKey("addressElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.House", "house")
                        .WithMany()
                        .HasForeignKey("houseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("addressElement");

                    b.Navigation("house");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Rating", b =>
                {
                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.Dish", "dish")
                        .WithOne("Rating")
                        .HasForeignKey("webNET_Hits_backend_aspnet_project_1.Models.Rating", "DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dish");

                    b.Navigation("user");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Dish", b =>
                {
                    b.Navigation("Rating")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
