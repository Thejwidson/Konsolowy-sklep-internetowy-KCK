﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sklep_Internetowy___Dawid_Szczawiński.Data;

#nullable disable

namespace Sklep_Internetowy___Dawid_Szczawiński.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20241118183510_Test1")]
    partial class Test1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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