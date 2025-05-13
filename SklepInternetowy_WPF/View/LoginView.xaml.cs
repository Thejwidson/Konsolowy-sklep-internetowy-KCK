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
            LoginProgressBar.Visibility = Visibility.Visible;
            MessageTextBlock.Text = ""; 
            await Task.Delay(2000); 

            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var user = _userController.Login(username, password);
            LoginProgressBar.Visibility = Visibility.Collapsed; 

            if (user != null)
            {
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                MessageTextBlock.Text = $"Welcome, {user.Login}!";
                await Task.Delay(1000); 

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
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = "Invalid credentials! Please try again.";
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            LoginProgressBar.Visibility = Visibility.Visible;
            MessageTextBlock.Text = "";

            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                LoginProgressBar.Visibility = Visibility.Collapsed;
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = "Nazwa użytkownika nie może być pusta.";
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                LoginProgressBar.Visibility = Visibility.Collapsed;
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = "Hasło nie może być puste.";
                return;
            }

            await Task.Delay(2000); 

            try
            {
                var user = _userController.Register(username, password);
                LoginProgressBar.Visibility = Visibility.Collapsed;

                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                MessageTextBlock.Text = $"Użytkownik {user.Login} został pomyślnie zarejestrowany!";
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                LoginProgressBar.Visibility = Visibility.Collapsed;
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = $"Rejestracja nie powiodła się: {ex.Message}";
            }
        }

        public void ConsoleAppButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "Sklep_Internetowy___Dawid_Szczawiński.exe", 
                UseShellExecute = true
            });
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }
    }
}
