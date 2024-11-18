using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Spectre.Console;
using System.Linq;


namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class AdminView
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;

        public AdminView(ProductController productController, ProductCategoryController productCategoryController)
        {
            _productController = productController;
            _productCategoryController = productCategoryController;
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
                    .AddChoices("Add Category", "Remove Category", "Add Product", "Remove Product", "Logout"));

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
                    case "Logout":
                        return;
                }
            }
        }

        private void AddCategory()
        {
            var name = AnsiConsole.Ask<string>("Category [green]name[/]:");
            _productCategoryController.AddCategory(name);
            AnsiConsole.MarkupLine("[green]Category added![/]\n");
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

            _productCategoryController.RemoveCategory(selectedCategory.ProductCategoryID);

            AnsiConsole.MarkupLine("[green]Category removed![/]\n");
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

            

            AnsiConsole.MarkupLine("[green]Product added![/]\n");
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

            _productController.RemoveProduct(selectedProduct.ProductID);
            AnsiConsole.MarkupLine("[green]Product removed![/]\n");
        }
    }

}
