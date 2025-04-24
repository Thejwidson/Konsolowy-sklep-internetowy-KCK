using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Spectre.Console;
using System.Linq;
using System.Configuration;
using System.Runtime.CompilerServices;


namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class AdminView
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly UserController _userController;

        public AdminView(ProductController productController, ProductCategoryController productCategoryController, UserController userController)
        {
            _productController = productController;
            _productCategoryController = productCategoryController;
            _userController = userController;

        }

        public void ShowAdminMenu()
        {
            AnsiConsole.Write(
                new FigletText("Admin Menu")
                .Centered()
                .Color(Color.Red)
                );

            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("Add Category", "Remove Category", "Add Product", "Remove Product", "Users", "Logout"));

                switch (choice)
                {
                    case "Add Category":
                        AddCategory();
                        break;
                    case "Remove Category":
                        RemoveCategory();
                        break;
                    case "Add Product":
                        AddProduct();
                        break;
                    case "Remove Product":
                        RemoveProduct();
                        break;
                    case "Users":
                        Users();
                        break;
                    case "Logout":
                        AnsiConsole.Clear();
                        return;
                }
            }
        }

        private void AddCategory()
        {
            var name = AnsiConsole.Ask<string>("Category [green]name[/]:");
            _productCategoryController.AddCategory(name);
            AnsiConsole.MarkupLine($"[green]Category '{name}' added![/]\n");
        }

        private void RemoveCategory() 
        {
            var categories = _productCategoryController.GetAllCategories();
            var selectedCategory = AnsiConsole.Prompt(
                new SelectionPrompt<ProductCategory>()
                    .Title("Category [green]name[/]?")
                    .PageSize(10)
                    .AddChoices(categories)
                    .UseConverter(c => c.Name));

            AnsiConsole.MarkupLine($"[green]Category '{selectedCategory.Name}' removed![/]\n");

            _productCategoryController.RemoveCategory(selectedCategory.ProductCategoryID);
        }

        private void AddProduct()
        {
            var name = AnsiConsole.Ask<string>("Product [green]name[/]:");
            var price = AnsiConsole.Ask<decimal>("Product [green]price[/]:");
            var categoryName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Product [green]category[/]?")
                    .PageSize(10)
                    .AddChoices(_productCategoryController.GetAllCategoriesNames()));

            _productController.AddProduct(name, price, categoryName);

            AnsiConsole.MarkupLine($"[green]Product '{name}' added![/]\n");
        }

        private void RemoveProduct()
        {
            var categories = _productCategoryController.GetAllCategories();
            var products = _productController.GetAllProducts();
            if (!products.Any())
            {
                AnsiConsole.MarkupLine("[red]No products available![/]\n");
                return;
            }

            var selectedProduct = AnsiConsole.Prompt(
                new SelectionPrompt<Product>()
                .Title("Select a product to remove")
                .AddChoices(products)
                .UseConverter(p => $"{p.ProductCategory.Name} - {p.Name} - {p.Price:C}"));

            AnsiConsole.MarkupLine($"[green]Product '{selectedProduct.Name}' removed![/]\n");

            _productController.RemoveProduct(selectedProduct.ProductID);
        }

        private void Users()
        {
            var users = _userController.GetUsers();
            if (!users.Any())
            {
                AnsiConsole.MarkupLine("[red]No users available![/]\n");
                return;
            }
            AnsiConsole.MarkupLine("[green]Users:[/]");
            foreach (var user in users)
            {
                AnsiConsole.MarkupLine($"[green]{user.Login}[/]");
            }
        }
    }

}
