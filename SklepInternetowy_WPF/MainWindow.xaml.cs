using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Hardcodet.Wpf.TaskbarNotification;

namespace SklepInternetowy_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ShopDbContext _context;
        private readonly UserController _userController;
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;
        private TaskbarIcon _trayIcon;


        public MainWindow()
        {
            InitializeComponent();

            _context = new ShopDbContext();
            _userController = new UserController(_context);
            _productController = new ProductController(_context);
            _productCategoryController = new ProductCategoryController(_context);
            _shoppingCartController = new ShoppingCartController(_context);
            _trayIcon = (TaskbarIcon)FindResource("MyTrayIcon");

            SwitchView(new View.LoginView(this, _userController));
        }

        public void SwitchView(object view)
        {
            MainContent.Content = view;
        }

        public void SwitchToAdminView()
        {
            var adminView = new View.AdminView(_productController, _productCategoryController, this, _userController);
            SwitchView(adminView);
        }

        public void SwitchToUserView(User currentUser)
        {
            var userView = new View.UserView(currentUser, _productController, _productCategoryController, _shoppingCartController, _userController, this);
            SwitchView(userView);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
                _trayIcon.Visibility = Visibility.Visible;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _trayIcon.Dispose(); // zwolnij zasoby
            base.OnClosed(e);
        }


    }
}
