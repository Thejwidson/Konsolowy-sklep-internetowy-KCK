using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using SklepInternetowy_WPF.Localization;
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
            InitializeLocalization();
            _shoppingCartController = shoppingCartController;
            _userId = userId;
            _mainWindow = mainWindow;
            _userController = userController;
            LoadOrders();

            // Subskrybuj zmiany języka
            LocalizationManager.Instance.PropertyChanged += OnLocalizationChanged;
        }

        private void InitializeLocalization()
        {
            // Ustawienie DataContext dla całego UserControl
            this.DataContext = LocalizationManager.Instance;
        }

        private void OnLocalizationChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Odśwież interfejs po zmianie języka
            if (e.PropertyName == "Item[]")
            {
                // W tym przypadku nie ma potrzeby odświeżania danych,
                // bo bindingi automatycznie się zaktualizują
            }
        }

        private async void LoadOrders()
        {
            var orders = await Task.Run(() => _shoppingCartController.GetAllOrders(_userId));
            OrdersListView.ItemsSource = orders;
        }

        private void BackToUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userController.GetUser(_userId);
            _mainWindow.SwitchToUserView(user);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Anuluj subskrypcję przy zniszczeniu kontrolki
            if (LocalizationManager.Instance != null)
            {
                LocalizationManager.Instance.PropertyChanged -= OnLocalizationChanged;
            }
        }
    }
}