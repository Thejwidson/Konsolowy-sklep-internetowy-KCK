using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.Model;

namespace Sklep_Internetowy___Dawid_Szczawiński.Controller
{
    public class ProductController
    {
        private readonly ShopDbContext _context;

        public ProductController(ShopDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(string name, decimal price, string category)
        {
            Product product = new Product
            {
                Name = name,
                Price = price,
                ProductCategory = _context.ProductCategories.FirstOrDefault(c => c.Name == category)
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public Product RemoveProduct(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ProductID == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return product;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.ProductCategory).ToList();
            //return _context.Products.ToList();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategory.ProductCategoryID == categoryId).ToList();
        }

        public List<Product> GetProductsByLowestPrice()
        {
            return _context.Products.OrderBy(p => p.Price).ToList();
        }

        public List<Product> GetProductsByHighestPrice()
        {
            return _context.Products.OrderByDescending(p => p.Price).ToList();
        }

        public List<Product> GetProductsByName(string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name)).ToList();
        }
    }
}
