using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        private readonly User _currentUser;
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;
        private readonly UserController _userController;
        private readonly MainWindow _mainWindow;

        public UserView(User currentUser, ProductController productController, ProductCategoryController productCategoryController, ShoppingCartController shoppingCartController, UserController userController, MainWindow mainWindow)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
            _userController = userController;
            _mainWindow = mainWindow;
            
        }


        private void ShowProducts()
        {
            // Widok produktów
            var productView = new ProductView(_productController, _productCategoryController, _shoppingCartController, _currentUser.UserID, _mainWindow, _userController);
            ContentArea.Content = productView;
        }

        /*private void ShowOrderHistory()
        {
            // Widok historii zamówień
            var orderHistoryView = new OrderHistoryView(_shoppingCartController, _currentUser.UserID);
            ContentArea.Content = orderHistoryView;
        }*/

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            // Przełącz widok na logowanie (przykład)
            var loginView = new LoginView(_mainWindow, _userController);
            ContentArea.Content = loginView;
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProducts();
        }

        private void ShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            var cartView = new ShoppingCartView(_productController, _productCategoryController, _shoppingCartController, _currentUser.UserID, _userController, _mainWindow);
            ContentArea.Content = cartView;
        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var orderHistoryView = new OrderHistoryView(_shoppingCartController, _currentUser.UserID, _mainWindow, _userController);
            ContentArea.Content = orderHistoryView;
        }
    }
}
