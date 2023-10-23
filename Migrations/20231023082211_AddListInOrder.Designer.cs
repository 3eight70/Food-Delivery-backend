﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webNET_Hits_backend_aspnet_project_1.Data;

#nullable disable

namespace webNET_Hits_backend_aspnet_project_1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231023082211_AddListInOrder")]
    partial class AddListInOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.DTO.DishBasketDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("DishBasketDTO");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

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

                    b.Property<double?>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.DishInCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("dishId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("userId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("DishesInCart");
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

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.Property<Guid>("userId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Rating", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("UserId", "DishId");

                    b.HasIndex("DishId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.DTO.DishBasketDTO", b =>
                {
                    b.HasOne("webNET_Hits_backend_aspnet_project_1.Models.Order", null)
                        .WithMany("DishesInCart")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("webNET_Hits_backend_aspnet_project_1.Models.Order", b =>
                {
                    b.Navigation("DishesInCart");
                });
#pragma warning restore 612, 618
        }
    }
}
