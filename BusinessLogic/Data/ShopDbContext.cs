using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CandyShop;Trusted_Connection=True;ConnectRetryCount=0");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCategoryID = 1, Name = "BLUZA" },
                new ProductCategory { ProductCategoryID = 2, Name = "KOSZULKA" },
                new ProductCategory { ProductCategoryID = 3, Name = "SPODNIE" },
                new ProductCategory { ProductCategoryID = 4, Name = "SPODENKI" },
                new ProductCategory { ProductCategoryID = 5, Name = "CZAPKA" },
                new ProductCategory { ProductCategoryID = 6, Name = "BUTY" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, Name = "Bluza z kapturem", Price = 149.99m, ProductCategoryID = 1 },
                new Product { ProductID = 2, Name = "Bluza sportowa", Price = 129.99m, ProductCategoryID = 1 },
                new Product { ProductID = 3, Name = "Bluza polarowa", Price = 199.99m, ProductCategoryID = 1 },
                new Product { ProductID = 4, Name = "Koszulka bawełna", Price = 49.99m, ProductCategoryID = 2 },
                new Product { ProductID = 5, Name = "Koszulka sportowa", Price = 69.99m, ProductCategoryID = 2 },
                new Product { ProductID = 6, Name = "Koszulka polo", Price = 89.99m, ProductCategoryID = 2 },
                new Product { ProductID = 7, Name = "Jeansy męskie", Price = 199.99m, ProductCategoryID = 3 },
                new Product { ProductID = 8, Name = "Spodnie dresowe", Price = 99.99m, ProductCategoryID = 3 },
                new Product { ProductID = 9, Name = "Spodnie materiał", Price = 149.99m, ProductCategoryID = 3 },
                new Product { ProductID = 10, Name = "Spodenki jeansowe", Price = 79.99m, ProductCategoryID = 4 },
                new Product { ProductID = 11, Name = "Spodenki sportowe", Price = 59.99m, ProductCategoryID = 4 },
                new Product { ProductID = 12, Name = "Spodenki plażowe", Price = 49.99m, ProductCategoryID = 4 },
                new Product { ProductID = 13, Name = "Czapka z daszkiem", Price = 39.99m, ProductCategoryID = 5 },
                new Product { ProductID = 14, Name = "Czapka zimowa", Price = 59.99m, ProductCategoryID = 5 },
                new Product { ProductID = 15, Name = "Czapka sportowa", Price = 49.99m, ProductCategoryID = 5 },
                new Product { ProductID = 16, Name = "Buty sportowe", Price = 299.99m, ProductCategoryID = 6 },
                new Product { ProductID = 17, Name = "Buty trekking", Price = 399.99m, ProductCategoryID = 6 },
                new Product { ProductID = 18, Name = "Buty casual", Price = 249.99m, ProductCategoryID = 6 },
                new Product { ProductID = 19, Name = "Bluza rozpinana", Price = 159.99m, ProductCategoryID = 1 },
                new Product { ProductID = 20, Name = "Koszulka termo", Price = 89.99m, ProductCategoryID = 2 },
                new Product { ProductID = 21, Name = "Spodnie bojówki", Price = 179.99m, ProductCategoryID = 3 },
                new Product { ProductID = 22, Name = "Spodenki dresowe", Price = 69.99m, ProductCategoryID = 4 },
                new Product { ProductID = 23, Name = "Czapka letnia", Price = 29.99m, ProductCategoryID = 5 },
                new Product { ProductID = 24, Name = "Buty zimowe", Price = 499.99m, ProductCategoryID = 6 },
                new Product { ProductID = 25, Name = "Buty eleganckie", Price = 349.99m, ProductCategoryID = 6 },
                new Product { ProductID = 26, Name = "Bluza sportowa z logo", Price = 139.99m, ProductCategoryID = 1 },
                new Product { ProductID = 27, Name = "Koszulka oversize", Price = 59.99m, ProductCategoryID = 2 },
                new Product { ProductID = 28, Name = "Spodnie slim fit", Price = 169.99m, ProductCategoryID = 3 },
                new Product { ProductID = 29, Name = "Spodenki treningowe", Price = 79.99m, ProductCategoryID = 4 },
                new Product { ProductID = 30, Name = "Czapka baseball", Price = 49.99m, ProductCategoryID = 5 }
            );

            modelBuilder.Entity<User>().HasData(
            
                new User {UserID = 1, Login = "admin", Password = "admin", isAdmin = true },
                new User { UserID = 2, Login = "user", Password = "user", isAdmin = false }

            );
        }

    }
}
