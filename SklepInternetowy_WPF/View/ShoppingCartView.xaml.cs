using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections.ObjectModel;
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
using SklepInternetowy_WPF.Localization;

namespace SklepInternetowy_WPF.View
{
    /// <summary>
    /// Interaction logic for ShoppingCartView.xaml
    /// </summary>
    public partial class ShoppingCartView : UserControl
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;
        private readonly int _userId;
        private readonly UserController _userController;
        private readonly MainWindow _mainWindow;

        public ObservableCollection<Product> CartProducts { get; set; }

        public ShoppingCartView(ProductController productController, ProductCategoryController productCategoryController, ShoppingCartController shoppingCartController, int userId, UserController userController, MainWindow mainWindow)
        {
            InitializeComponent();
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
            _userId = userId;
            _userController = userController;
            _mainWindow = mainWindow;

            // Subskrypcja na zmiany lokalizacji
            LocalizationManager.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Item[]")
                {
                    UpdateLocalization();
                }
            };

            LoadCartProducts();
        }

        private void LoadCartProducts()
        {
            var cart = _shoppingCartController.GetOrCreateCart(_userId);
            CartProductsListView.ItemsSource = null;
            CartProductsListView.ItemsSource = cart.Products;

            var totalPrice = _shoppingCartController.GetProductsPriceSum(_userId);
            TotalPriceTextBlock.Text = $"{totalPrice:C}";
        }

        private async void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is Product product)
            {
                _shoppingCartController.RemoveProductFromCart(_userId, product.ProductID);

                // Używamy zlokalizowanej wiadomości
                var message = string.Format(LocalizationManager.Instance["ProductRemovedFromCart"], product.Name);
                SetStatusMessage(message, false);

                await Task.Delay(3000);
                LoadCartProducts();
            }
        }

        private async void FinalizeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = await Task.Run(() => _shoppingCartController.FinalizeOrder(_userId));

                // Używamy zlokalizowanej wiadomości
                var message = string.Format(LocalizationManager.Instance["OrderFinalized"], order.Price.ToString("C"));
                SetStatusMessage(message, false);

                await Task.Delay(3000);
                LoadCartProducts();
            }
            catch (InvalidOperationException ex)
            {
                SetStatusMessage(ex.Message, true);
            }
        }

        private void CartProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = CartGridView;
            if (listView == null || gridView == null) return;

            var totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            if (totalWidth > 0)
            {
                gridView.Columns[0].Width = totalWidth * 0.4;
                gridView.Columns[1].Width = totalWidth * 0.3;
                gridView.Columns[2].Width = totalWidth * 0.3;
            }
        }

        private async void SetStatusMessage(string message, bool isError = false)
        {
            StatusMessageTextBlock.Text = message;
            StatusMessageTextBlock.Foreground = isError
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Colors.Green);

            await Task.Delay(3000);
            StatusMessageTextBlock.Text = "";
        }

        private void BackToUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userController.GetUser(_userId);
            _mainWindow.SwitchToUserView(user);
        }

        // Metoda do aktualizacji lokalizacji (wywołana przy zmianie języka)
        private void UpdateLocalization()
        {
            // Wymuszenie odświeżenia bindingów - XAML automatycznie zaktualizuje teksty
            // dzięki bindingom do LocalizationManager
            LoadCartProducts(); // Odświeżenie może być potrzebne dla TotalPrice
        }
    }
}

