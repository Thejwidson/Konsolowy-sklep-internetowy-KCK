using Sklep_Internetowy___Dawid_Szczawiński.Controller;
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
    /// Interaction logic for OrderHistoryView.xaml
    /// </summary>
    public partial class OrderHistoryView : UserControl
    {
        private readonly ShoppingCartController _shoppingCartController;
        private readonly int _userId;
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;

        public OrderHistoryView(ShoppingCartController shoppingCartController, int userId, MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            _shoppingCartController = shoppingCartController;
            _userId = userId;
            _mainWindow = mainWindow;
            _userController = userController;

            LoadOrders();
        }

        private void LoadOrders()
        {
            // Pobierz zamówienia użytkownika
            var orders = _shoppingCartController.GetAllOrders(_userId);

            // Przypisz do ListView
            OrdersListView.ItemsSource = orders;
        }

        private void BackToUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userController.GetUser(_userId);
            _mainWindow.SwitchToUserView(user);
        }
    }
}
