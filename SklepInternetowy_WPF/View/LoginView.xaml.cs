using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SklepInternetowy_WPF.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;


        public LoginView(MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _userController = userController;
            _mainWindow = mainWindow;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetl ProgressBar
            LoginProgressBar.Visibility = Visibility.Visible;
            MessageTextBlock.Text = ""; // Wyczyść komunikaty
            await Task.Delay(2000); // Symulacja opóźnienia 2 sekund

            // Pobierz dane logowania
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var user = _userController.Login(username, password);
            LoginProgressBar.Visibility = Visibility.Collapsed; // Ukryj ProgressBar

            if (user != null)
            {
                // Wyświetl komunikat o sukcesie
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                MessageTextBlock.Text = $"Welcome, {user.Login}!";
                await Task.Delay(1000); // Opóźnienie przed przełączeniem widoku

                // Przełącz widok
                if (user.isAdmin)
                {
                    _mainWindow.SwitchToAdminView();
                }
                else
                {
                    _mainWindow.SwitchToUserView(user);
                }
            }
            else
            {
                // Wyświetl komunikat o błędzie
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = "Invalid credentials! Please try again.";
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetl ProgressBar
            LoginProgressBar.Visibility = Visibility.Visible;
            MessageTextBlock.Text = ""; // Wyczyść komunikaty
            await Task.Delay(2000); // Symulacja opóźnienia 2 sekund

            // Pobierz dane rejestracji
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                var user = _userController.Register(username, password);
                LoginProgressBar.Visibility = Visibility.Collapsed; // Ukryj ProgressBar

                // Wyświetl komunikat o sukcesie
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                MessageTextBlock.Text = $"User {user.Login} registered successfully!";
                await Task.Delay(1000); // Opóźnienie dla wizualnego efektu
            }
            catch (Exception ex)
            {
                // Wyświetl komunikat o błędzie
                LoginProgressBar.Visibility = Visibility.Collapsed; // Ukryj ProgressBar
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = $"Registration failed: {ex.Message}";
            }
        }

        public void ConsoleAppButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "Sklep_Internetowy___Dawid_Szczawiński.exe", // Nazwa pliku aplikacji konsolowej
                UseShellExecute = true
            });
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }
    }
}
