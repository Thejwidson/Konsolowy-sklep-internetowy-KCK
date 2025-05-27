using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using SklepInternetowy_WPF.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;

        public AdminView(ProductController productController, ProductCategoryController productCategoryController, MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            _productController = productController;
            _productCategoryController = productCategoryController;

            LoadCategories();
            LoadProducts();

            _mainWindow = mainWindow;
            _userController = userController;
            LoadUsers();

            // Nasłuchiwanie zmian w lokalizacji
            LocalizationManager.Instance.PropertyChanged += OnLocalizationChanged;
        }

        private void OnLocalizationChanged(object sender, PropertyChangedEventArgs e)
        {
            // Odświeżenie bindingów po zmianie języka
            if (e.PropertyName == "Item[]")
            {
                // Wymuś odświeżenie wszystkich bindingów
                this.DataContext = null;
                this.DataContext = this;
            }
        }

        private void LoadCategories()
        {
            var categories = _productCategoryController.GetAllCategories();
            ProductCategoryComboBox.ItemsSource = categories;
            CategoryRadioList.ItemsSource = categories;
        }

        // Załadowanie kategorii do ComboBox
        /*private void LoadCategories()
        {
            var categories = _productCategoryController.GetAllCategories();
            CategoryComboBox.ItemsSource = categories;
            ProductCategoryComboBox.ItemsSource = categories;
        }*/

        //ComboBox
        private async void LoadProducts()
        {
            var products = await Task.Run(() => _productController.GetAllProducts());
            ProductComboBox.ItemsSource = products.Select(p => new
            {
                Name = $"{p.ProductCategory.Name} - {p.Name} - {p.Price:C}",
                Product = p
            }).ToList();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = CategoryNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseEnterValidCategoryName"], true);
                return;
            }

            _productCategoryController.AddCategory(categoryName);
            LoadCategories();
            SetStatusMessage(string.Format(LocalizationManager.Instance["CategoryHasBeenAdded"], categoryName));
            CategoryNameTextBox.Clear();
        }

        /*private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCategory = (ProductCategory)CategoryComboBox.SelectedItem;
            if (selectedCategory == null)
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseSelectCategoryToRemove"], true);
                return;
            }

            _productCategoryController.RemoveCategory(selectedCategory.ProductCategoryID);
            LoadCategories();
            SetStatusMessage(string.Format(LocalizationManager.Instance["CategoryHasBeenRemoved"], selectedCategory.Name));
        }*/

        private void RemoveSingleCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ProductCategory selectedCategory = null;

            foreach (var item in CategoryRadioList.Items)
            {
                var container = (ContentPresenter)CategoryRadioList.ItemContainerGenerator.ContainerFromItem(item);

                if (container == null) continue;

                var radioButton = FindVisualChild<RadioButton>(container);

                if (radioButton != null && radioButton.IsChecked == true)
                {
                    selectedCategory = (ProductCategory)radioButton.Tag;
                    break;
                }
            }

            if (selectedCategory == null)
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseSelectCategoryToRemove"], true);
                return;
            }

            var result = MessageBox.Show(
                string.Format(LocalizationManager.Instance["ConfirmCategoryRemoval"], selectedCategory.Name),
                LocalizationManager.Instance["ConfirmDeletion"],
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result != MessageBoxResult.Yes)
            {
                SetStatusMessage(LocalizationManager.Instance["RemovalCancelled"]);
                return;
            }

            _productCategoryController.RemoveCategory(selectedCategory.ProductCategoryID);

            LoadCategories();
            SetStatusMessage(string.Format(LocalizationManager.Instance["CategoryHasBeenRemoved"], selectedCategory.Name));
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T tChild)
                {
                    return tChild;
                }

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var productName = ProductNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(productName))
            {
                SetStatusMessage(LocalizationManager.Instance["ProductNameCannotBeEmpty"], true);
                return;
            }

            if (!decimal.TryParse(ProductPriceTextBox.Text, out var productPrice))
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseEnterValidPrice"], true);
                return;
            }

            if (productPrice <= 0)
            {
                SetStatusMessage(LocalizationManager.Instance["PriceMustBeGreaterThanZero"], true);
                return;
            }

            if (ProductCategoryComboBox.SelectedItem is not ProductCategory selectedCategory)
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseSelectValidCategory"], true);
                return;
            }

            _productController.AddProduct(productName, productPrice, selectedCategory.Name);
            LoadProducts();
            SetStatusMessage(string.Format(LocalizationManager.Instance["ProductHasBeenAdded"], productName));

            ProductNameTextBox.Clear();
            ProductPriceTextBox.Clear();
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductComboBox.SelectedItem;
            if (selectedProduct == null)
            {
                SetStatusMessage(LocalizationManager.Instance["PleaseSelectProductToRemove"], true);
                return;
            }

            var product = (selectedProduct as dynamic).Product;

            var result = MessageBox.Show(
                string.Format(LocalizationManager.Instance["ConfirmProductRemoval"], product.Name),
                LocalizationManager.Instance["ConfirmDeletion"],
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result != MessageBoxResult.Yes)
            {
                SetStatusMessage(LocalizationManager.Instance["ProductRemovalCancelled"]);
                return;
            }

            _productController.RemoveProduct(product.ProductID);
            LoadProducts();
            SetStatusMessage(string.Format(LocalizationManager.Instance["ProductHasBeenRemoved"], product.Name));
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

        private void LoadUsers()
        {
            try
            {
                var users = _userController?.GetUsers();

                if (UserListBox == null)
                {
                    MessageBox.Show(LocalizationManager.Instance["ErrorUserListBoxNull"], LocalizationManager.Instance["Error"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (users == null)
                {
                    MessageBox.Show(LocalizationManager.Instance["ErrorGetUsersNull"], LocalizationManager.Instance["Error"], MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                UserListBox.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(LocalizationManager.Instance["ErrorLoadingUsers"], ex.Message), LocalizationManager.Instance["Error"], MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView(_mainWindow, _userController);
            _mainWindow.MainContent.Content = loginView;
        }
    }
}