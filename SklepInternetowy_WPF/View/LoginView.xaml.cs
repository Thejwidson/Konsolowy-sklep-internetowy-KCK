using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using SklepInternetowy_WPF.Localization;
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
    public partial class LoginView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;

        public LoginView(MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _userController = userController;

            // Set default language
            LanguageComboBox.SelectedIndex = 0;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string languageCode = selectedItem.Tag.ToString();
                LocalizationManager.Instance.SetCulture(languageCode);
            }
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
                MessageTextBlock.Text = string.Format(LocalizationManager.Instance["WelcomeMessage"], user.Login);
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
                MessageTextBlock.Text = LocalizationManager.Instance["InvalidCredentials"];
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
                MessageTextBlock.Text = LocalizationManager.Instance["UsernameEmpty"];
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                LoginProgressBar.Visibility = Visibility.Collapsed;
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = LocalizationManager.Instance["PasswordEmpty"];
                return;
            }

            await Task.Delay(2000);

            try
            {
                var user = _userController.Register(username, password);
                LoginProgressBar.Visibility = Visibility.Collapsed;

                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                MessageTextBlock.Text = string.Format(LocalizationManager.Instance["RegistrationSuccess"], user.Login);
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                LoginProgressBar.Visibility = Visibility.Collapsed;
                MessageTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                MessageTextBlock.Text = string.Format(LocalizationManager.Instance["RegistrationFailed"], ex.Message);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }
    }
}
