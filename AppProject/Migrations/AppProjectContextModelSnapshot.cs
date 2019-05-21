using System;
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AppProject.Models.Colors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorName");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("AppProject.Models.ConnectTable", b =>
                {
                    b.Property<int>("ProductesId");

                    b.Property<int>("ColorId");

                    b.Property<int>("SizeId");

                    b.Property<int>("MartId");

                    b.HasKey("ProductesId", "ColorId", "SizeId", "MartId");

                    b.HasIndex("ColorId");

                    b.HasIndex("MartId");

                    b.HasIndex("SizeId");

                    b.ToTable("ConnectTable");
                });

            modelBuilder.Entity("AppProject.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<int>("CreditCard");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mail");

                    b.Property<string>("Password");

                    b.Property<int>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AppProject.Models.Mart", b =>
                {
                    b.Property<int>("Id");

                    b.HasKey("Id");

                    b.ToTable("Mart");
                });

            modelBuilder.Entity("AppProject.Models.Productes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountInStock");

                    b.Property<int>("AmountOfOrders");

                    b.Property<double>("DeliveryPrice");

                    b.Property<string>("ImgId");

                    b.Property<double>("Price");

                    b.Property<string>("ProductName");

                    b.Property<int?>("SubCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Productes");
                });

            modelBuilder.Entity("AppProject.Models.Sizes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SizeName");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("AppProject.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoriesId");

                    b.Property<string>("SubName");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("AppProject.Models.ConnectTable", b =>
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
                        .WithOne("Mart")
                        .HasForeignKey("AppProject.Models.Mart", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppProject.Models.Productes", b =>
                {
                    b.HasOne("AppProject.Models.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("AppProject.Models.SubCategory", b =>
                {
                    b.HasOne("AppProject.Models.Categories", "Categories")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoriesId");
                });
        }
    }
}
