using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.View;
//using SklepInternetowy_WPF/ Namespace aplikacji WPF

using System.Windows; // Wymagane dla Application
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;
using Spectre.Console;
using System.Diagnostics;

class Program
{
    static void Main()
    {     
        using var context = new ShopDbContext();
        //context.Database.Migrate();

        var userController = new UserController(context);
        var productController = new ProductController(context);
        var productCategoryController = new ProductCategoryController(context);
        var shoppingCartController = new ShoppingCartController(context);

        var loginView = new LoginView(userController);

        while (true)
        {
            var user = loginView.ShowLogin();
            if (user == null) continue;

            if (user.isAdmin)
            {
                var adminView = new AdminView(productController, productCategoryController, userController);
                Console.Clear();
                adminView.ShowAdminMenu();
            }
            else
            {
                var userView = new UserView(user, productController,productCategoryController, shoppingCartController);
                Console.Clear();
                userView.ShowUserMenu();
            }
        }

        

        static void StartWpfApp()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "SklepInternetowy_WPF.exe", // Nazwa pliku WPF (upewnij się, że jest poprawna)
                UseShellExecute = true
            });
        }

    }
}
