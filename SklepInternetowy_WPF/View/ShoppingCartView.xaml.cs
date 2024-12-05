using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;
using System.Collections.Generic;
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
            LoadCartProducts();
        }
        private void LoadCartProducts()
        {
            // Pobieramy koszyk użytkownika
            var cart = _shoppingCartController.GetOrCreateCart(_userId);
            CartProductsListView.ItemsSource = null;
            CartProductsListView.ItemsSource = cart.Products;

            // Obliczamy i wyświetlamy cenę całkowitą
            var totalPrice = _shoppingCartController.GetProductsPriceSum(_userId);
            TotalPriceTextBlock.Text = $"{totalPrice:C}";

        }

        private async void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is Product product)
            {
                _shoppingCartController.RemoveProductFromCart(_userId, product.ProductID);
                SetStatusMessage($"Produkt {product.Name} został usunięty z koszyka.", false);
                await Task.Delay(3000);
                LoadCartProducts(); // Odśwież listę produktów
            }
        }

        private async void FinalizeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = _shoppingCartController.FinalizeOrder(_userId);

                
                SetStatusMessage($"Zamówienie zostało złożone. Cena całkowita: {order.Price:C}.", false);
                await Task.Delay(3000);
                LoadCartProducts(); // Opróżnij koszyk po finalizacji
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

            // Obliczamy dostępne miejsce na kolumny
            var totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // Odejmujemy scrollbar
            if (totalWidth > 0)
            {
                // Proporcjonalne dostosowanie szerokości kolumn
                gridView.Columns[0].Width = totalWidth * 0.4; // 40% dla "Nazwa"
                gridView.Columns[1].Width = totalWidth * 0.3; // 30% dla "Cena"
                gridView.Columns[2].Width = totalWidth * 0.3; // 30% dla "Akcja"
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
    }
}

