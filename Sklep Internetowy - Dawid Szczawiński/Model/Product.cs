using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public int ProductCategoryID { get; set; } //
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
