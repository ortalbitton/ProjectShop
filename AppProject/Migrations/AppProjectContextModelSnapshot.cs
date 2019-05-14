﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AppProject.Models;

namespace AppProject.Migrations
{
    [DbContext(typeof(AppProjectContext))]
    partial class AppProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppProject.Models.Categories", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AppProject.Models.Colors", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorName");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("AppProject.Models.ConectTable", b =>
                {
                    b.Property<int>("DetailsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColorId");

                    b.Property<int>("MartId");

                    b.Property<int>("ProductesId");

                    b.Property<int>("SizeId");

                    b.HasKey("DetailsId");

                    b.HasIndex("ColorId");

                    b.HasIndex("MartId");

                    b.HasIndex("ProductesId");

                    b.HasIndex("SizeId");

                    b.ToTable("ConectTable");
                });

            modelBuilder.Entity("AppProject.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<int>("CreditCard");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mail");

                    b.Property<int>("MartId");

                    b.Property<string>("Password");

                    b.Property<int>("PhoneNumber");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AppProject.Models.Mart", b =>
                {
                    b.Property<int>("MartId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.HasKey("MartId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Mart");
                });

            modelBuilder.Entity("AppProject.Models.Productes", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountInStock");

                    b.Property<int>("AmountOfOrders");

                    b.Property<double>("DeliveryPrice");

                    b.Property<string>("ImgId");

                    b.Property<double>("Price");

                    b.Property<string>("ProductName");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("ProductId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Productes");
                });

            modelBuilder.Entity("AppProject.Models.Sizes", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SizeName");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("AppProject.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriesId");

                    b.Property<string>("SubName");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("AppProject.Models.ConectTable", b =>
                {
                    b.HasOne("AppProject.Models.Colors", "Color")
                        .WithMany("Details")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppProject.Models.Mart", "Mart")
                        .WithMany("Details")
                        .HasForeignKey("MartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppProject.Models.Productes", "Productes")
                        .WithMany("Details")
                        .HasForeignKey("ProductesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppProject.Models.Sizes", "Size")
                        .WithMany("Details")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppProject.Models.Mart", b =>
                {
                    b.HasOne("AppProject.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppProject.Models.Productes", b =>
                {
                    b.HasOne("AppProject.Models.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppProject.Models.SubCategory", b =>
                {
                    b.HasOne("AppProject.Models.Categories", "Categories")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
