﻿using Sklep_Internetowy___Dawid_Szczawiński.Controller;
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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        private readonly ProductController _productController;
        private readonly ProductCategoryController _productCategoryController;
        private readonly MainWindow _mainWindow;
        private readonly UserController _userController;

        // Konstruktor przyjmujący kontrolery jako argumenty
        public AdminView(ProductController productController, ProductCategoryController productCategoryController, MainWindow mainWindow, UserController userController)
        {
            InitializeComponent();
            _productController = productController;
            _productCategoryController = productCategoryController;

            LoadCategories();
            LoadProducts();
            _mainWindow = mainWindow;
            _userController = userController;
        }

        // Załadowanie kategorii do ComboBox
        private void LoadCategories()
        {
            var categories = _productCategoryController.GetAllCategories();
            CategoryComboBox.ItemsSource = categories;
            ProductCategoryComboBox.ItemsSource = categories;
        }

        // Załadowanie produktów do ComboBox
        private void LoadProducts()
        {
            var products = _productController.GetAllProducts();
            ProductComboBox.ItemsSource = products.Select(p => new
            {
                Name = $"{p.ProductCategory.Name} - {p.Name} - {p.Price:C}",
                Product = p
            }).ToList();
        }

        // Obsługa przycisku do dodawania kategorii
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = CategoryNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                SetStatusMessage("Please enter a valid category name.", true);
                return;
            }

            _productCategoryController.AddCategory(categoryName);
            LoadCategories();
            SetStatusMessage($"Category '{categoryName}' has been added.");
            CategoryNameTextBox.Clear();
        }

        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCategory = (ProductCategory)CategoryComboBox.SelectedItem;
            if (selectedCategory == null)
            {
                SetStatusMessage("Please select a category to remove.", true);
                return;
            }

            _productCategoryController.RemoveCategory(selectedCategory.ProductCategoryID);
            LoadCategories();
            SetStatusMessage($"Category '{selectedCategory.Name}' has been removed.");
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var productName = ProductNameTextBox.Text.Trim();
            if (!decimal.TryParse(ProductPriceTextBox.Text, out var productPrice))
            {
                SetStatusMessage("Please enter a valid price.", true);
                return;
            }

            var selectedCategory = (ProductCategory)ProductCategoryComboBox.SelectedItem;
            if (selectedCategory == null)
            {
                SetStatusMessage("Please select a valid category.", true);
                return;
            }

            _productController.AddProduct(productName, productPrice, selectedCategory.Name);
            LoadProducts();
            SetStatusMessage($"Product '{productName}' has been added.");
            ProductNameTextBox.Clear();
            ProductPriceTextBox.Clear();
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductComboBox.SelectedItem;
            if (selectedProduct == null)
            {
                SetStatusMessage("Please select a product to remove.", true);
                return;
            }

            var product = (selectedProduct as dynamic).Product;
            _productController.RemoveProduct(product.ProductID);
            LoadProducts();
            SetStatusMessage($"Product '{product.Name}' has been removed.");
        }

        // Pomocnicza metoda ustawiania komunikatów
        private async void SetStatusMessage(string message, bool isError = false)
        {
            StatusMessageTextBlock.Text = message;
            StatusMessageTextBlock.Foreground = isError
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Colors.Green);

            await Task.Delay(3000);
            StatusMessageTextBlock.Text = "";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView(_mainWindow, _userController);
            _mainWindow.MainContent.Content = loginView;
        }
    }
}

