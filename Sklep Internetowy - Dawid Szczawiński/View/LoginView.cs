using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using Spectre.Console;

namespace Sklep_Internetowy___Dawid_Szczawiński.View
{
    public class LoginView
    {
        private readonly UserController _userController;

        public LoginView(UserController userController) => _userController = userController;

        public User ShowLogin()
        {
            AnsiConsole.Write(
                new FigletText("Candy Shop")
                .Centered()
                .Color(Color.Red)
                );


            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Login or Register?")
                .AddChoices("Login", "Register","WPF_APP","Exit"));

            switch (choice)
            {
                case "Login":
                    return Login();
                case "Register":
                    return Register();
                case "WPF_APP":
                    StartWpfApp();
                    return null;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
            return null;
        }

        private User Login()
        {
            var username = AnsiConsole.Ask<string>("Enter [green]username[/]:");
            var password = AnsiConsole.Ask<string>("Enter [green]password[/]:");
            var user = _userController.Login(username, password);
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start("[yellow]Logging in...[/]", ctx => {
                    Thread.Sleep(1000);
                });
            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid credentials![/]");
                return user;
            }
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start("[green]Credentials Confirmed! Changing the view! [/]", ctx => {
                    Thread.Sleep(2000);
                });
            return user;
        }

        private User Register()
        {
            var username = AnsiConsole.Ask<string>("Choose [green]username[/]:");
            var password = AnsiConsole.Ask<string>("Choose [green]password[/]:");
            var user = _userController.Register(username, password);
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start("[yellow]Registering...[/]", ctx => {
                    Thread.Sleep(1000);
                });
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start("[green]Registration successful! Changing the view! [/]", ctx => {
                    Thread.Sleep(2000);
                });
            return user;
        }

        private void StartWpfApp()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "SklepInternetowy_WPF.exe", // Plik wykonywalny WPF
                    UseShellExecute = true
                });
                Environment.Exit(0); // Opcjonalnie zamknij aplikację konsolową po uruchomieniu WPF
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error starting WPF application: {ex.Message}[/]");
            }
        }
    }

}
