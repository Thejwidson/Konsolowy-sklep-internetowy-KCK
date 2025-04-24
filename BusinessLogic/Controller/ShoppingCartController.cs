using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy___Dawid_Szczawiński.Data;
using Sklep_Internetowy___Dawid_Szczawiński.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

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

            GenerateReceipt(order);

            return order;
        }

        public decimal GetProductsPriceSum(int userId)
        {
            var cart = GetOrCreateCart(userId);
            return cart.Products.Sum(p => p.Price);
        }

        private void GenerateReceipt(Order order)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, $"Receipt_{order.OrderDate:yyyyMMdd_HHmmss}.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                iTextSharp.text.Font titleFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 16);
                iTextSharp.text.Font normalFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 12);

                doc.Add(new iTextSharp.text.Paragraph("Paragon", titleFont));
                doc.Add(new iTextSharp.text.Paragraph($"Data: {order.OrderDate:yyyy-MM-dd HH:mm:ss}", normalFont));
                doc.Add(new iTextSharp.text.Paragraph($"ID zamówienia: {order.OrderID}", normalFont));
                doc.Add(new iTextSharp.text.Paragraph("\n"));

                PdfPTable table = new PdfPTable(3);
                table.AddCell("Produkt");
                table.AddCell("Cena");
                table.AddCell("Ilość");

                foreach (var product in order.Products)
                {
                    table.AddCell(product.Name);
                    table.AddCell($"{product.Price:C}");
                    table.AddCell("1");
                }

                doc.Add(table);
                doc.Add(new iTextSharp.text.Paragraph("\n"));
                doc.Add(new iTextSharp.text.Paragraph($"Suma: {order.Price:C}", titleFont));

                doc.Close();
                writer.Close();
            }

            Console.WriteLine($"Paragon zapisany jako: {filePath}");
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
