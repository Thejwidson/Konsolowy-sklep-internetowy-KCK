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

    }
}
