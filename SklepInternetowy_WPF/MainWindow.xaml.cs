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


        public MainWindow()
        {
            InitializeComponent();

            _context = new ShopDbContext();
            _userController = new UserController(_context);
            _productController = new ProductController(_context);
            _productCategoryController = new ProductCategoryController(_context);
            _shoppingCartController = new ShoppingCartController(_context);

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


    }
}
