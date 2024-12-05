using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;

namespace SklepInternetowy_WPF.View
{
    public partial class ProductView : UserControl
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly ShoppingCartController _shoppingCartController;
        private readonly int _userId;
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;

        public ProductView(ProductController productController, ProductCategoryController productCategoryController, ShoppingCartController shoppingCartController, int userId, MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            InitializeComboBox();
            _mainWindow = mainWindow;
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
            _userId = userId;

            LoadCategories();
            LoadProducts();
            _userController = userController;
        }

        private void LoadCategories()
        {
            // Pobierz kategorie z kontrolera
            var categories = _productCategoryController.GetAllCategoriesNames();

            // Dodaj "Wszystkie kategorie" jako pierwszą opcję
            FilterByCategoryComboBox.Items.Clear();
            FilterByCategoryComboBox.Items.Add("Wszystkie kategorie");
            foreach (var category in categories)
            {
                FilterByCategoryComboBox.Items.Add(category);
            }

            // Ustaw domyślną wartość
            FilterByCategoryComboBox.SelectedIndex = 0;
        }
        
        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = ProductsGridView;
            if (listView == null || gridView == null) return;

            // Oblicz dostępne miejsce w ListView
            var totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // Odejmujemy scrollbar
            if (totalWidth > 0)
            {
                // Dynamiczne ustawianie szerokości kolumn
                gridView.Columns[0].Width = totalWidth * 0.3; // 40% dla "Nazwa"
                gridView.Columns[1].Width = totalWidth * 0.2; // 20% dla "Cena"
                gridView.Columns[2].Width = totalWidth * 0.3; // 30% dla "Kategoria"
                gridView.Columns[3].Width = totalWidth * 0.2; // 10% dla "Akcja"
            }
        }

        private void InitializeComboBox()
        {
            foreach (var item in FilterByCategoryComboBox.Items)
            {
                if (item is ComboBoxItem comboBoxItem)
                {
                    comboBoxItem.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    comboBoxItem.VerticalContentAlignment = VerticalAlignment.Center;
                }
            }
        }

        private void LoadProducts()
        {
            var products = _productController.GetAllProducts();
            ProductsListView.ItemsSource = products;
        }

        private void BackToUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userController.GetUser(_userId);
            _mainWindow.SwitchToUserView(user);
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredProducts = _productController.GetAllProducts();

            // Filtrowanie po nazwie
            if (!string.IsNullOrWhiteSpace(FilterByNameTextBox.Text))
            {
                filteredProducts = _productController.GetProductsByName(FilterByNameTextBox.Text);
            }

            // Filtrowanie po kategorii
            if (FilterByCategoryComboBox.SelectedIndex > 0) // Pomijamy "Wszystkie kategorie"
            {
                var selectedCategory = FilterByCategoryComboBox.SelectedItem.ToString();
                var categoryObj = _productCategoryController.GetAllCategories()
                    .FirstOrDefault(c => c.Name == selectedCategory);
                if (categoryObj != null)
                {
                    filteredProducts = _productController.GetProductsByCategory(categoryObj.ProductCategoryID);
                }
            }

            // Sortowanie po cenie
            if (SortByPriceComboBox.SelectedIndex == 1) // Cena rosnąco
            {
                filteredProducts = _productController.GetProductsByLowestPrice();
            }
            else if (SortByPriceComboBox.SelectedIndex == 2) // Cena malejąco
            {
                filteredProducts = _productController.GetProductsByHighestPrice();
            }

            // Przypisz przefiltrowane produkty do ListView
            ProductsListView.ItemsSource = filteredProducts;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is Product product)
            {
                _shoppingCartController.AddProductToCart(_userId, product);
                MessageBox.Show($"Produkt {product.Name} został dodany do koszyka!", "Dodano do koszyka", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
