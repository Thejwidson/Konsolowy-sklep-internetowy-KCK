using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Spectre.Console;

namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class UserView
    {
        private readonly User _currentUser;
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;


        public UserView(User currentUser, ProductController productController,ProductCategoryController productCategoryController, ShoppingCartController shoppingCartController)
        {
            _currentUser = currentUser;
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
        }

        public void ShowUserMenu()
        {
            AnsiConsole.Write(
                new FigletText("User Menu")
                    .Centered()
                    .Color(Color.Red)
                );

            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("Products","Shopping Cart", "View Order History", "Logout"));

                switch (choice)
                {
                    case "Products":
                        AnsiConsole.Clear();
                        var productView = new ProductView(_productController, _productCategoryController);
                        productView.ShowProductMenu();
                        break;
                    case "Shopping Cart":
                        AnsiConsole.Clear();
                        var cartView = new ShoppingCartView(_productController, _productCategoryController, _shoppingCartController, _currentUser.UserID);
                        cartView.ShowCartMenu();
                        break;
                    case "View Order History":
                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("Order History")
                                .Centered()
                                .Color(Color.Red)
                        );
                        ViewOrderHistory();
                        break;
                    
                    case "Logout":
                        AnsiConsole.Clear();
                        return;
                }
            }
        }

        private void ViewOrderHistory()
        {
            var orders = _shoppingCartController.GetAllOrders(_currentUser.UserID);

            var table = new Table().Centered();
            table.AddColumn("[green]ID[/]");
            table.AddColumn("[blue]Date[/]");
            table.AddColumn("[red]Price[/]");

            foreach (var order in orders)
            {
                table.AddRow($"[green]{order.OrderID}[/]", $"[blue]{order.OrderDate.ToString("g")}[/]", $"[red]{order.Price:C}[/]");
            }

            AnsiConsole.Render(table);

            var choices = orders.Select(o => o.OrderID).ToList();
            choices.Add(0);

            var selectedOrderId = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Select an Order or 0 to exit")
                    .AddChoices(choices)
            );

            var selectedOrder = orders.FirstOrDefault(o => o.OrderID == selectedOrderId);

            if (selectedOrderId == 0) 
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                new FigletText("User Menu")
                    .Centered()
                    .Color(Color.Red)
                );
                return;
            }

            if (selectedOrder != null)
            {
                ShowOrderDetails(selectedOrder); 
            }
        }

        private void ShowOrderDetails(Order selectedOrder)
        {
            if (selectedOrder.Products == null || !selectedOrder.Products.Any())
            {
                AnsiConsole.Markup("\n[red]No products found in this order![/]\n\n");
                return;
            }

            var table = new Table().Centered();
            table.AddColumn("[green]Product Name[/]");
            table.AddColumn("[blue]Price[/]");

            foreach (var product in selectedOrder.Products)
            {
                table.AddRow($"[green]{product.Name}[/]", $"[blue]{product.Price:C}[/]");
            }
            AnsiConsole.Render(table);

            return;

        }
    }
}
