using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using System;

namespace Sklep_Internetowy___Dawid_Szczawiński.Controller
{

    public class ShoppingCartController
    {
        private readonly ShopDbContext _context;

        public ShoppingCartController(ShopDbContext context)
        {
            _context = context;
        }

        public ShoppingCart GetOrCreateCart(int userID)
        {
            var cart = _context.ShoppingCarts.Include(c => c.Products).FirstOrDefault(c => c.UserID == userID);
            if (cart == null)
            {
                cart = new ShoppingCart { UserID = userID };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();
            }
            return cart;
        }

        public void AddProductToCart(int userId, Product product)
        {
            var cart = GetOrCreateCart(userId);
            cart.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProductFromCart(int userId, int productId)
        {
            var cart = GetOrCreateCart(userId);
            var product = cart.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public Order FinalizeOrder(int userId)
        {
            var cart = GetOrCreateCart(userId);
            if (!cart.Products.Any())
                throw new InvalidOperationException("Shopping cart is empty!");

            var order = new Order
            {
                UserID = userId,
                Products = new List<Product>(cart.Products),
                OrderDate = DateTime.Now,
                Price = cart.Products.Sum(p => p.Price)
            };

            _context.Orders.Add(order);
            cart.Products.Clear();
            _context.SaveChanges();

            return order;
        }

        public decimal GetProductsPriceSum(int userId)
        {
            var cart = GetOrCreateCart(userId);
            return cart.Products.Sum(p => p.Price);
        }

        public List<Order> GetAllOrders(int userId)
        {
            return _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.Products) 
                .ToList();
        }
    }
}
