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
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Kolory
                var primaryColor = new iTextSharp.text.BaseColor(102, 0, 204); // fioletowy
                var lightGray = new iTextSharp.text.BaseColor(240, 240, 240); // jasnoszary
                var softBlue = new iTextSharp.text.BaseColor(173, 216, 230); // pastelowy niebieski

                // Czcionki
                var titleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 20, primaryColor);
                var subtitleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.BaseColor.DARK_GRAY);
                var normalFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.BaseColor.BLACK);
                var boldFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, primaryColor);

                // Nagłówek
                var storeName = new Paragraph("Paragon - Sklep CandyShop", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                doc.Add(storeName);

                var address = new Paragraph("ul. Wiejska 45A, 15-351 Białystok\nNIP: 542-020-87-21", subtitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(address);

                // Informacje o zamówieniu
                PdfPTable orderInfoTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                orderInfoTable.SetWidths(new float[] { 1, 2 });

                PdfPCell infoCell(string text, iTextSharp.text.Font font, bool alignRight = false)
                {
                    return new PdfPCell(new Phrase(text, font))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = alignRight ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT,
                        PaddingBottom = 5
                    };
                }

                orderInfoTable.AddCell(infoCell("Data:", boldFont));
                orderInfoTable.AddCell(infoCell($"{order.OrderDate:yyyy-MM-dd HH:mm:ss}", normalFont));
                orderInfoTable.AddCell(infoCell("ID zamówienia:", boldFont));
                orderInfoTable.AddCell(infoCell($"{order.OrderID}", normalFont));
                doc.Add(orderInfoTable);

                doc.Add(new Paragraph("\n"));

                // Tabela produktów
                PdfPTable productTable = new PdfPTable(4)
                {
                    WidthPercentage = 100
                };
                productTable.SetWidths(new float[] { 4, 2, 1, 2 });

                // Nagłówki tabeli
                void AddHeaderCell(string text)
                {
                    var cell = new PdfPCell(new Phrase(text, boldFont))
                    {
                        BackgroundColor = softBlue,
                        Padding = 8,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    productTable.AddCell(cell);
                }

                AddHeaderCell("Produkt");
                AddHeaderCell("Cena");
                AddHeaderCell("Ilość");
                AddHeaderCell("Suma");

                // Produkty
                bool isEvenRow = true;
                foreach (var product in order.Products)
                {
                    BaseColor backgroundColor = isEvenRow ? lightGray : BaseColor.WHITE;
                    isEvenRow = !isEvenRow;

                    PdfPCell productCell(string text)
                    {
                        return new PdfPCell(new Phrase(text, normalFont))
                        {
                            BackgroundColor = backgroundColor,
                            Padding = 5,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                    }

                    productTable.AddCell(productCell(product.Name));
                    productTable.AddCell(productCell($"{product.Price:C}"));
                    productTable.AddCell(productCell("1"));
                    productTable.AddCell(productCell($"{product.Price:C}"));
                }

                doc.Add(productTable);

                // Suma
                PdfPTable totalTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20f
                };
                totalTable.SetWidths(new float[] { 4, 1 });

                var totalLabelCell = new PdfPCell(new Phrase("Suma:", boldFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingTop = 10
                };
                var totalValueCell = new PdfPCell(new Phrase($"{order.Price:C}", titleFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingTop = 10
                };

                totalTable.AddCell(totalLabelCell);
                totalTable.AddCell(totalValueCell);

                doc.Add(totalTable);

                // Stopka
                var footerBackground = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 30f
                };

                var footerCell = new PdfPCell(new Phrase("Dziękujemy za zakupy w Candyshop!", boldFont))
                {
                    BackgroundColor = softBlue,
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 10
                };
                footerBackground.AddCell(footerCell);

                doc.Add(footerBackground);

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
