﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public User User { get; set; }
        public int UserID { get; set; }
    }
}
