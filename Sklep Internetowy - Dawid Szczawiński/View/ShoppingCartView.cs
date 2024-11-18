using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class ShoppingCartView
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;
        private readonly int _userId;

        public ShoppingCartView(ProductController productController, ProductCategoryController productCategoryController, ShoppingCartController shoppingCartController, int userId)
        {
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
            _userId = userId;
        }

        public void ShowCartMenu()
        {
            AnsiConsole.Write(
                new FigletText("Shopping Cart")
                    .Centered()
                    .Color(Color.Red)
            );

            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("View Cart", "Add Product to Cart", "Remove Product from Cart", "Checkout", "Back"));

                switch (choice)
                {
                    case "View Cart":
                        ViewCart();
                        break;
                    case "Add Product to Cart":
                        AddProductToCart();
                        break;
                    case "Remove Product from Cart":
                        RemoveProductFromCart();
                        break;
                    case "Checkout":
                        Checkout();
                        return;  
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

        private void ViewCart()
        {
            var cart = _shoppingCartController.GetOrCreateCart(_userId);
            var categories = _productCategoryController.GetAllCategories();
            AnsiConsole.MarkupLine($"[bold]Shopping Cart[/]");

            if (!cart.Products.Any())
            {
                AnsiConsole.MarkupLine("[italic]Your cart is empty.[/]");
                return;
            }

            var table = new Table().Centered();
            table.AddColumn("[green]Category[/]");
            table.AddColumn("[blue]Name[/]");
            table.AddColumn("[red]Price[/]");

            foreach (var product in cart.Products)
            {
                table.AddRow($"[green]{product.ProductCategory.Name}[/]", $"[blue]{product.Name}[/]", $"[red]{product.Price:C}[/]");
            }

            AnsiConsole.Render(table);


            
        }

        private void AddProductToCart()
        {
            var products = _productController.GetAllProducts();
            var categories = _productCategoryController.GetAllCategories();
            var selectedProduct = AnsiConsole.Prompt(
                new SelectionPrompt<Product>()
                .Title("Select a product to add to cart")
                .AddChoices(products)
                .UseConverter(p => $"{p.ProductCategory.Name} - {p.Name} - {p.Price:C}"));

            _shoppingCartController.AddProductToCart(_userId, selectedProduct);
            AnsiConsole.MarkupLine("[green]Product added to cart![/]");
        }

        private void RemoveProductFromCart()
        {
            var categories = _productCategoryController.GetAllCategories();
            var cart = _shoppingCartController.GetOrCreateCart(_userId);
            if (!cart.Products.Any())
            {
                AnsiConsole.MarkupLine("[red]Your cart is empty.[/]");
                return;
            }

            var selectedProduct = AnsiConsole.Prompt(
                new SelectionPrompt<Product>()
                .Title("Select a product to remove from cart")
                .AddChoices(cart.Products)
                .UseConverter(p => $"{p.ProductCategory.Name} - {p.Name} - {p.Price:C}"));

            _shoppingCartController.RemoveProductFromCart(_userId, selectedProduct.ProductID);
            AnsiConsole.MarkupLine("[green]Product removed from cart![/]");
        }

        private void Checkout()
        {
            try
            {
                var order = _shoppingCartController.FinalizeOrder(_userId);
                AnsiConsole.MarkupLine("[green]Order placed successfully![/]");
            }
            catch (InvalidOperationException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }
    }
}
