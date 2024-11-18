﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sklep_Internetowy___Dawid_Szczawiński.Data;

#nullable disable

namespace Sklep_Internetowy___Dawid_Szczawiński.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductCategoryID")
                        .HasColumnType("int");

                    b.Property<int?>("ShoppingCartID")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductCategoryID");

                    b.HasIndex("ShoppingCartID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Name = "Bluza z kapturem",
                            Price = 149.99m,
                            ProductCategoryID = 1
                        },
                        new
                        {
                            ProductID = 2,
                            Name = "Bluza sportowa",
                            Price = 129.99m,
                            ProductCategoryID = 1
                        },
                        new
                        {
                            ProductID = 3,
                            Name = "Bluza polarowa",
                            Price = 199.99m,
                            ProductCategoryID = 1
                        },
                        new
                        {
                            ProductID = 4,
                            Name = "Koszulka bawełna",
                            Price = 49.99m,
                            ProductCategoryID = 2
                        },
                        new
                        {
                            ProductID = 5,
                            Name = "Koszulka sportowa",
                            Price = 69.99m,
                            ProductCategoryID = 2
                        },
                        new
                        {
                            ProductID = 6,
                            Name = "Koszulka polo",
                            Price = 89.99m,
                            ProductCategoryID = 2
                        },
                        new
                        {
                            ProductID = 7,
                            Name = "Jeansy męskie",
                            Price = 199.99m,
                            ProductCategoryID = 3
                        },
                        new
                        {
                            ProductID = 8,
                            Name = "Spodnie dresowe",
                            Price = 99.99m,
                            ProductCategoryID = 3
                        },
                        new
                        {
                            ProductID = 9,
                            Name = "Spodnie materiał",
                            Price = 149.99m,
                            ProductCategoryID = 3
                        },
                        new
                        {
                            ProductID = 10,
                            Name = "Spodenki jeansowe",
                            Price = 79.99m,
                            ProductCategoryID = 4
                        },
                        new
                        {
                            ProductID = 11,
                            Name = "Spodenki sportowe",
                            Price = 59.99m,
                            ProductCategoryID = 4
                        },
                        new
                        {
                            ProductID = 12,
                            Name = "Spodenki plażowe",
                            Price = 49.99m,
                            ProductCategoryID = 4
                        },
                        new
                        {
                            ProductID = 13,
                            Name = "Czapka z daszkiem",
                            Price = 39.99m,
                            ProductCategoryID = 5
                        },
                        new
                        {
                            ProductID = 14,
                            Name = "Czapka zimowa",
                            Price = 59.99m,
                            ProductCategoryID = 5
                        },
                        new
                        {
                            ProductID = 15,
                            Name = "Czapka sportowa",
                            Price = 49.99m,
                            ProductCategoryID = 5
                        },
                        new
                        {
                            ProductID = 16,
                            Name = "Buty sportowe",
                            Price = 299.99m,
                            ProductCategoryID = 6
                        },
                        new
                        {
                            ProductID = 17,
                            Name = "Buty trekking",
                            Price = 399.99m,
                            ProductCategoryID = 6
                        },
                        new
                        {
                            ProductID = 18,
                            Name = "Buty casual",
                            Price = 249.99m,
                            ProductCategoryID = 6
                        },
                        new
                        {
                            ProductID = 19,
                            Name = "Bluza rozpinana",
                            Price = 159.99m,
                            ProductCategoryID = 1
                        },
                        new
                        {
                            ProductID = 20,
                            Name = "Koszulka termo",
                            Price = 89.99m,
                            ProductCategoryID = 2
                        },
                        new
                        {
                            ProductID = 21,
                            Name = "Spodnie bojówki",
                            Price = 179.99m,
                            ProductCategoryID = 3
                        },
                        new
                        {
                            ProductID = 22,
                            Name = "Spodenki dresowe",
                            Price = 69.99m,
                            ProductCategoryID = 4
                        },
                        new
                        {
                            ProductID = 23,
                            Name = "Czapka letnia",
                            Price = 29.99m,
                            ProductCategoryID = 5
                        },
                        new
                        {
                            ProductID = 24,
                            Name = "Buty zimowe",
                            Price = 499.99m,
                            ProductCategoryID = 6
                        },
                        new
                        {
                            ProductID = 25,
                            Name = "Buty eleganckie",
                            Price = 349.99m,
                            ProductCategoryID = 6
                        },
                        new
                        {
                            ProductID = 26,
                            Name = "Bluza sportowa z logo",
                            Price = 139.99m,
                            ProductCategoryID = 1
                        },
                        new
                        {
                            ProductID = 27,
                            Name = "Koszulka oversize",
                            Price = 59.99m,
                            ProductCategoryID = 2
                        },
                        new
                        {
                            ProductID = 28,
                            Name = "Spodnie slim fit",
                            Price = 169.99m,
                            ProductCategoryID = 3
                        },
                        new
                        {
                            ProductID = 29,
                            Name = "Spodenki treningowe",
                            Price = 79.99m,
                            ProductCategoryID = 4
                        },
                        new
                        {
                            ProductID = 30,
                            Name = "Czapka baseball",
                            Price = 49.99m,
                            ProductCategoryID = 5
                        });
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductCategoryID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductCategoryID");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            ProductCategoryID = 1,
                            Name = "BLUZA"
                        },
                        new
                        {
                            ProductCategoryID = 2,
                            Name = "KOSZULKA"
                        },
                        new
                        {
                            ProductCategoryID = 3,
                            Name = "SPODNIE"
                        },
                        new
                        {
                            ProductCategoryID = 4,
                            Name = "SPODENKI"
                        },
                        new
                        {
                            ProductCategoryID = 5,
                            Name = "CZAPKA"
                        },
                        new
                        {
                            ProductCategoryID = 6,
                            Name = "BUTY"
                        });
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartID"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ShoppingCartID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.Order", b =>
                {
                    b.HasOne("Sklep_Internetowy___Dawid_Szczawiński.Model.User", "User")
                        .WithMany("OrderList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.Product", b =>
                {
                    b.HasOne("Sklep_Internetowy___Dawid_Szczawiński.Model.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderID");

                    b.HasOne("Sklep_Internetowy___Dawid_Szczawiński.Model.ProductCategory", "ProductCategory")
                        .WithMany("products")
                        .HasForeignKey("ProductCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sklep_Internetowy___Dawid_Szczawiński.Model.ShoppingCart", null)
                        .WithMany("Products")
                        .HasForeignKey("ShoppingCartID");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.ShoppingCart", b =>
                {
                    b.HasOne("Sklep_Internetowy___Dawid_Szczawiński.Model.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Sklep_Internetowy___Dawid_Szczawiński.Model.ShoppingCart", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.ProductCategory", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.ShoppingCart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Sklep_Internetowy___Dawid_Szczawiński.Model.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("OrderList");
                });
#pragma warning restore 612, 618
        }
    }
}
