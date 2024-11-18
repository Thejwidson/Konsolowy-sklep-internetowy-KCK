using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.Model;

namespace Sklep_Internetowy___Dawid_Szczawiński.Controller
{
    public class ProductCategoryController
    {
        private readonly ShopDbContext _context;

        public ProductCategoryController(ShopDbContext context)
        {
            _context = context;
        }

        public ProductCategory AddCategory(string name)
        {
            ProductCategory category = new ProductCategory
            {
                Name = name
            };

            _context.ProductCategories.Add(category);
            _context.SaveChanges();

            return category;
        }

        public ProductCategory RemoveCategory(int id)
        {
            ProductCategory category = _context.ProductCategories.FirstOrDefault(c => c.ProductCategoryID == id);

            if (category != null)
            {
                _context.ProductCategories.Remove(category);
                _context.SaveChanges();
            }

            return category;
        }

        public List<string> GetAllCategoriesNames()
        {
            return _context.ProductCategories.Select(c => c.Name).ToList();
        }

        public List<ProductCategory> GetAllCategories()
        {
            return _context.ProductCategories.ToList();
        }
    }
}