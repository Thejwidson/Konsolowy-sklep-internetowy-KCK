using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Internetowy___Dawid_Szczawiński.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Money { get; set; }
        public bool isAdmin { get; set; } = true;
        public List<Order> OrderList { get; set; } = new List<Order>();
        public ShoppingCart Cart { get; set; }


    }
}
