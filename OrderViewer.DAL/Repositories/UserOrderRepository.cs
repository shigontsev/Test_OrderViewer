using Microsoft.EntityFrameworkCore;
using OrderViewer.Common.Entities;
using OrderViewer.Common.Helpers;
using OrderViewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        //private readonly ApplicationDBContext _db;
        private ApplicationDBContext _db;
        //public UserOrderRepository(ApplicationDBContext db) 
        //{
        //    _db = db;
        //}
        //public UserOrderRepository()
        //{
        //    _db = new ApplicationDBContext();
        //}

        public bool CreateUser(string login, string password)
        {
            //var u = GetUser(login);
            if (string.IsNullOrWhiteSpace(login) || GetUser(login) is not null)
            {
                return false;
            }
            var user = new UserData()
            {
                Name = login,
                HashPass = Hasher.HashPassword(password)
            };
            using (_db = new ApplicationDBContext())
            {
                _db.UserData.Add(user);
                _db.SaveChanges();
            }

            return true;
        }

        public bool IsAuthentication(string login, string password)
        {
            using (_db = new ApplicationDBContext())
            {
                var accountData = _db.UserData.FirstOrDefault(x => x.Name == login);
                if (accountData != null)
                {
                    return Hasher.VerifyHashedPassword(accountData.HashPass, password);
                }
            }
            return false;
        }

        public UserData GetUser(int id)
        {
            //UserData user = (UserData)(from u in _db.UserData
            //                           where u.Id == id
            //                           select new
            //                           {
            //                               Id = u.Id,
            //                               Name = u.Name
            //                           });
            //return user;
            using (_db = new ApplicationDBContext())
            {
                UserData user = _db.UserData.FirstOrDefault(x => x.Id == id);
                return new UserData()
                {
                    Id = user.Id,
                    Name = user.Name
                };
            }
        }

        public UserData GetUser(string username)
        {
            using (_db = new ApplicationDBContext())
            {
                UserData user = _db.UserData.FirstOrDefault(x => x.Name == username);
                //UserData user = (from u in _db.UserData
                //                           where u.Name == username
                //                           select new
                //                           {
                //                               Id = u.Id,
                //                               Name = u.Name
                //                           }) as UserData;
                return new UserData()
                {
                    Id = user.Id,
                    Name = user.Name
                };
            }
            //return user;
        }

        public IEnumerable<UserData> GetUsers()
        {
            using (var db = new ApplicationDBContext())
            {
                var users =
                    (IEnumerable<UserData>)(from u in _db.UserData
                                            select new
                                            {
                                                Id = u.Id,
                                                Name = u.Name
                                            });
                return users;
            }
        }

        public bool CreateOrder(int user_id, IEnumerable<Product> products)
        {
            using(var db = new ApplicationDBContext())
            {
                var user = GetUser(user_id);
                if (user == null)
                {
                    return false;
                }
                //foreach (var product in products)
                //{
                //    if (_db.Product.All()
                //    {

                //    }
                //}
                if (!products.All(x=>_db.Product.FirstOrDefault(y=>y.Id==x.Id) is not null)) 
                {
                    return false;
                }

                _db.Order.Add(new Order()
                {
                    UserDataId = user_id,
                });
                _db.SaveChanges();

                Order order = _db.Order.Last(x=>x.UserDataId == user_id);
                foreach (var product in products)
                {
                    _db.OrderProduct.Add(new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductId = product.Id
                    });
                }
                _db.SaveChanges();

                return true;
            }
        }

        public bool CreateOrder(string user_name, IEnumerable<Product> products)
        {
            UserData user = GetUser(user_name);

            return CreateOrder(user.Id, products);
        }
    }
}
