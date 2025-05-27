using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Sklep_Internetowy___Dawid_Szczawiński.Controller;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using SklepInternetowy_WPF.Localization;

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
            InitializeLocalization();
            InitializeComboBox();
            _mainWindow = mainWindow;
            _productController = productController;
            _productCategoryController = productCategoryController;
            _shoppingCartController = shoppingCartController;
            _userId = userId;

            LoadCategories();
            LoadProducts();
            _userController = userController;

            // Subskrybuj zmiany języka
            LocalizationManager.Instance.PropertyChanged += OnLocalizationChanged;
        }

        private void InitializeLocalization()
        {
            // Ustawienie DataContext dla całego UserControl, aby używać LocalizationManager
            this.DataContext = LocalizationManager.Instance;
        }

        private void OnLocalizationChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Odśwież kategorie po zmianie języka
            if (e.PropertyName == "Item[]")
            {
                LoadCategories();
                RefreshSortingComboBox();
            }
        }

        private void RefreshSortingComboBox()
        {
            var selectedIndex = SortByPriceComboBox.SelectedIndex;

            SortByPriceComboBox.Items.Clear();

            var noSortingItem = new ComboBoxItem();
            noSortingItem.SetBinding(ComboBoxItem.ContentProperty, new System.Windows.Data.Binding("Item[NoSorting]")
            {
                Source = LocalizationManager.Instance
            });
            SortByPriceComboBox.Items.Add(noSortingItem);

            var priceAscItem = new ComboBoxItem();
            priceAscItem.SetBinding(ComboBoxItem.ContentProperty, new System.Windows.Data.Binding("Item[PriceAscending]")
            {
                Source = LocalizationManager.Instance
            });
            SortByPriceComboBox.Items.Add(priceAscItem);

            var priceDescItem = new ComboBoxItem();
            priceDescItem.SetBinding(ComboBoxItem.ContentProperty, new System.Windows.Data.Binding("Item[PriceDescending]")
            {
                Source = LocalizationManager.Instance
            });
            SortByPriceComboBox.Items.Add(priceDescItem);

            SortByPriceComboBox.SelectedIndex = selectedIndex;
        }

        private void LoadCategories()
        {
            var categories = _productCategoryController.GetAllCategoriesNames();
            var selectedIndex = FilterByCategoryComboBox.SelectedIndex;

            FilterByCategoryComboBox.Items.Clear();

            // Dodaj "Wszystkie kategorie" z lokalizacją
            var allCategoriesItem = new ComboBoxItem();
            allCategoriesItem.SetBinding(ComboBoxItem.ContentProperty, new System.Windows.Data.Binding("Item[AllCategories]")
            {
                Source = LocalizationManager.Instance
            });
            FilterByCategoryComboBox.Items.Add(allCategoriesItem);

            // Dodaj pozostałe kategorie
            foreach (var category in categories)
            {
                FilterByCategoryComboBox.Items.Add(new ComboBoxItem { Content = category });
            }

            FilterByCategoryComboBox.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = ProductsGridView;
            if (listView == null || gridView == null) return;

            //dostępne miejsce w ListView
            var totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            if (totalWidth > 0)
            {
                // Dynamiczne ustawianie szerokości kolumn
                gridView.Columns[0].Width = totalWidth * 0.3; // 30% dla nazwy
                gridView.Columns[1].Width = totalWidth * 0.2; // 20% dla ceny
                gridView.Columns[2].Width = totalWidth * 0.3; // 30% dla kategorii
                gridView.Columns[3].Width = totalWidth * 0.2; // 20% dla akcji
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

        private async void LoadProducts()
        {
            var products = await Task.Run(() => _productController.GetAllProducts());
            ProductsListView.ItemsSource = products;
        }

        private void BackToUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userController.GetUser(_userId);
            _mainWindow.SwitchToUserView(user);
        }

        private async void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredProducts = await Task.Run(() => _productController.GetAllProducts());

            if (!string.IsNullOrWhiteSpace(FilterByNameTextBox.Text))
            {
                filteredProducts = _productController.GetProductsByName(FilterByNameTextBox.Text);
            }

            if (FilterByCategoryComboBox.SelectedIndex > 0)
            {
                var selectedItem = FilterByCategoryComboBox.SelectedItem as ComboBoxItem;
                var selectedCategory = selectedItem?.Content?.ToString();

                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    var categoryObj = _productCategoryController.GetAllCategories()
                        .FirstOrDefault(c => c.Name == selectedCategory);
                    if (categoryObj != null)
                    {
                        filteredProducts = _productController.GetProductsByCategory(categoryObj.ProductCategoryID);
                    }
                }
            }

            if (SortByPriceComboBox.SelectedIndex == 1)
            {
                filteredProducts = _productController.GetProductsByLowestPrice();
            }
            else if (SortByPriceComboBox.SelectedIndex == 2)
            {
                filteredProducts = _productController.GetProductsByHighestPrice();
            }

            ProductsListView.ItemsSource = filteredProducts;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is Product product)
            {
                _shoppingCartController.AddProductToCart(_userId, product);

                var container = ProductsListView.ItemContainerGenerator.ContainerFromItem(product) as ListViewItem;
                if (container != null)
                {
                    // Użyj zlokalizowanego tekstu
                    var localizedMessage = string.Format(LocalizationManager.Instance["AddedToCart"], product.Name);
                    container.Tag = localizedMessage;
                }

                ClearTagAfterDelay(container, System.TimeSpan.FromSeconds(3));
            }
        }

        private async void ClearTagAfterDelay(ListViewItem item, System.TimeSpan delay)
        {
            await Task.Delay(delay);
            if (item != null)
            {
                item.Tag = string.Empty;
            }
        }

        // Pamiętaj o anulowaniu subskrypcji przy zniszczeniu kontrolki
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (LocalizationManager.Instance != null)
            {
                LocalizationManager.Instance.PropertyChanged -= OnLocalizationChanged;
            }
        }
    }
}