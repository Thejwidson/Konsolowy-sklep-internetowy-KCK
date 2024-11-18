using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class ProductView
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;


        public ProductView(ProductController productController, ProductCategoryController productCategoryController)
        {
            _productController = productController;
            _productCategoryController = productCategoryController;
        }

        public void ShowProductMenu()
        {
            AnsiConsole.Write(
                new FigletText("Product Menu")
                .Centered()
                .Color(Color.Red)
                );

            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("View All Products", "View Products By Name", "View Products By Category", "View Products By Lowest Price", "View Products By Highest Price", "Back"));

                switch (choice)
                {
                    case "View All Products":
                        AnsiConsole.Clear();
                        ViewProducts();
                        break;
                    case "View Products By Name":
                        AnsiConsole.Clear();
                        ViewProductsByName();
                        break;
                    case "View Products By Category":
                        AnsiConsole.Clear();
                        ViewProductsByCategory();
                        break;
                    case "View Products By Lowest Price":
                        AnsiConsole.Clear();
                        ViewProductsByLowestPrice();
                        break;
                    case "View Products By Highest Price":
                        AnsiConsole.Clear();
                        ViewProductsByHighestPrice();
                        break;
                    case "Back":
                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("User Menu")
                                .Centered()
                                .Color(Color.Red)
                        );
                        return;
                }
            }
        }

        private void ViewProducts()
        {
            var products = _productController.GetAllProducts();
            var categories = _productCategoryController.GetAllCategories();
            DisplayPagedProducts(products);
        }

        private void ViewProductsByName()
        {
            var name = AnsiConsole.Ask<string>("Product name?");
            var products = _productController.GetProductsByName(name);
            var categories = _productCategoryController.GetAllCategories();
            DisplayPagedProducts(products);
        }

        private void ViewProductsByCategory()
        {
            var categories = _productCategoryController.GetAllCategories();
            var selectedCategory = AnsiConsole.Prompt(
                new SelectionPrompt<ProductCategory>()
                    .Title("Category name?")
                    .PageSize(10)
                    .AddChoices(categories)
                    .UseConverter(c => c.Name));

            var products = _productController.GetProductsByCategory(selectedCategory.ProductCategoryID);
            DisplayPagedProducts(products);
        }

        private void ViewProductsByLowestPrice()
        {
            var categories = _productCategoryController.GetAllCategories();
            var products = _productController.GetProductsByLowestPrice();
            DisplayPagedProducts(products);
        }

        private void ViewProductsByHighestPrice()
        {
            var categories = _productCategoryController.GetAllCategories();
            var products = _productController.GetProductsByHighestPrice();
            DisplayPagedProducts(products);
        }

        private void DisplayPagedProducts(List<Product> products)
        {
            int pageIndex = 0;
            int pageSize = 9;  
            int totalPages = (int)Math.Ceiling(products.Count / (double)pageSize);
            var categories = _productCategoryController.GetAllCategories();

            while (true)
            {
                var pageProducts = products.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                var table = new Table().Centered();
                table.Border(TableBorder.None);
                int columnCount = 3;

                for (int i = 0; i < columnCount; i++)
                {
                    table.AddColumn(new TableColumn(""));
                }

                var productRows = pageProducts.Select((product, index) => new { product, index })
                                              .GroupBy(x => x.index / columnCount)
                                              .Select(group => group.Select(x => x.product).ToArray())
                                              .ToArray();

                foreach (var productRow in productRows)
                {
                    var rowPanels = productRow.Select(product =>
                        new Panel($"[green]Category:[/] {product.ProductCategory.Name}\n" +
                                  $"[blue]Name:[/] {product.Name}\n" +
                                  $"[red]Price:[/] {product.Price:C}")
                            .Border(BoxBorder.Rounded)
                            .BorderColor(Color.Yellow)
                            .Expand());

                    table.AddRow(rowPanels.Cast<IRenderable>().ToArray());
                }

                AnsiConsole.Render(table);


                var pageChoices = new List<string> { "Back" };

                if (pageIndex > 0) pageChoices.Insert(0, "Previous Page"); 
                if (pageIndex < totalPages - 1) pageChoices.Add("Next Page"); 

                var pageChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices(pageChoices)
                    .Title(""));

                switch (pageChoice)
                {
                    case "Previous Page":
                        AnsiConsole.Clear();
                        if (pageIndex > 0) pageIndex--;
                        break;
                    case "Next Page":
                        AnsiConsole.Clear();
                        if (pageIndex < totalPages - 1) pageIndex++;
                        break;
                    case "Back":
                        AnsiConsole.Clear();
                        
                        AnsiConsole.Write(
                            new FigletText("Product Menu")
                                .Centered()
                                .Color(Color.Red)
                        );

                        return;
                }
            }
        }
    }
}


