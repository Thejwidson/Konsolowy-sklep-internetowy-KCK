using Sklep_Internetowy___Dawid_Szczawiński.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Internetowy___Dawid_Szczawiński.Model;

namespace Sklep_Internetowy___Dawid_Szczawiński.Controller
{
    public class UserController
    {
        private readonly ShopDbContext _context;

        public UserController(ShopDbContext context)
        {
            _context = context;
        }

        public User Register(string login, string password)
        {
            User user = new User
            {
                Login = login,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Login(string login, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            return user;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == id);
        }


        public List<User> GetUsers()
        {

            string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "log.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    if (_context == null)
                    {
                        sw.WriteLine($"{DateTime.Now} - Error: _context is null!");
                        return new List<User>();
                    }

                    if (_context.Users == null)
                    {
                        sw.WriteLine($"{DateTime.Now} - Error: _context.Users is null!");
                        return new List<User>();
                    }

                    var users = _context.Users.Where(u => u.isAdmin == false).ToList();

                    if (users == null)
                    {
                        sw.WriteLine($"{DateTime.Now} - Error: users list is null!");
                        return new List<User>();
                    }

                    sw.WriteLine($"{DateTime.Now} - Loaded {users.Count} users.");
                    return users;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now} - Database error: {ex.Message}{Environment.NewLine}");
                return new List<User>();
            }
        }





    }
}
