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
            using (_db = new ApplicationDBContext())
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
            using(_db = new ApplicationDBContext())
            {
                //var user = GetUser(user_id);
                var user = _db.UserData.FirstOrDefault(x=>x.Id == user_id);
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
                //using(var db = new ApplicationDBContext())
                //{
                    if (!products.All(x => _db.Product.FirstOrDefault(y => y.Id == x.Id) is not null))
                    {
                        return false;
                    }
                //}
                //foreach (var item in products)
                //{
                //    var product = _db.Product.FirstOrDefault(y => y.Id == item.Id);
                //    if (product == null)
                //    {
                //        return false;
                //    }
                //}

                _db.Order.Add(new Order()
                {
                    UserDataId = user_id,
                });
                _db.SaveChanges();

                Order order = _db.Order.OrderByDescending(p => p.Id).FirstOrDefault(x=>x.UserDataId == user_id);
                if (order == null)
                {
                    return false;
                }
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


        #region errais 
        public IEnumerable<UserOrder> GetAllOrders()
        {
            var orders = new List<UserOrder>();
            using (_db = new ApplicationDBContext())
            {
                if (_db.OrderProduct.Count() > 0)
                
                    orders = (List<UserOrder>)
                        (from user in _db.UserData
                             join order in _db.Order on user.Id equals order.UserDataId  
                             join op in _db.OrderProduct on order.Id equals op.OrderId
                             join p in _db.Product on op.ProductId equals p.Id
                             select new UserOrder
                             {
                                 Id = op.Id,
                                 UserId = user.Id,
                                 UserName = user.Name,
                                 OrderId = order.Id,
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 Description = p.Description,
                                 Price = p.Price
                             }).ToList();
                return orders;
            }
        }

        public IEnumerable<UserOrder> GetOrdersByUserId(int user_id)
        {
            var orders = new List<UserOrder>();
            using (_db = new ApplicationDBContext())
            {
                if (_db.OrderProduct.Count() > 0)

                    orders = (List<UserOrder>)
                        (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where user_id == user.Id
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).ToList();
                return orders;
            }
        }

        public IEnumerable<UserOrder> GetOrderById(int order_id)
        {
            var result = new List<UserOrder>();
            using (_db = new ApplicationDBContext())
            {
                if (_db.OrderProduct.Count() > 0)

                    result = (List<UserOrder>)
                        (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where order_id == order.Id
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).ToList();
                return result;
            }
        }

        #endregion errais 

        public IEnumerable<OrderInfoShort> GetAllOrdersShort()
        {
            using (_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).GroupBy(x => x.OrderId);

                var orders = new List<OrderInfoShort>();
                foreach (var g in a)
                {
                    var ori = new OrderInfoShort()
                    {
                        Id = g.Key,
                        UserId = g.First().UserId,
                        UserName = g.First().UserName,
                        Count = g.Count(),
                        Sum = g.Sum(x => x.Price)
                    };
                    orders.Add(ori);
                }

                return orders.OrderBy(x=>x.Sum);

            }
        }

        public IEnumerable<OrderInfoFull> GetAllOrdersFull()
        {
            using (_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).GroupBy(x => x.OrderId);

                var orders = new List<OrderInfoFull>();
                foreach (var g in a)
                {
                    var ori = new OrderInfoFull()
                    {
                        Id = g.Key,
                        UserId = g.First().UserId,
                        UserName = g.First().UserName,
                        Products = new List<Product>()
                    };
                    foreach (var item in g)
                    {
                        ori.Products.Add(new Product()
                        {
                            Id = item.ProductId,
                            Name = item.ProductName,
                            Description = item.Description,
                            Price = item.Price,
                        });
                    }
                    ori.Products = ori.Products.OrderBy(x => x.Price).ToList();
                    orders.Add(ori);
                }

                return orders;

            }
        }

        public IEnumerable<OrderInfoShort> GetAllOrdersShortByUserId(int user_id)
        {
            using (_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where (user.Id == user_id)
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).GroupBy(x => x.OrderId);

                var orders = new List<OrderInfoShort>();
                foreach (var g in a)
                {
                    var ori = new OrderInfoShort()
                    {
                        Id = g.Key,
                        UserId = g.First().UserId,
                        UserName = g.First().UserName,
                        Count = g.Count(),
                        Sum = g.Sum(x => x.Price)
                    };
                    orders.Add(ori);
                }

                return orders.OrderBy(x => x.Sum);

            }
        }

        public IEnumerable<OrderInfoFull> GetAllOrdersFullByUserId(int user_id)
        {
            using (_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where (user.Id == user_id)
                         select new UserOrder
                         {
                             Id = op.Id,
                             UserId = user.Id,
                             UserName = user.Name,
                             OrderId = order.Id,
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Description = p.Description,
                             Price = p.Price
                         }).GroupBy(x => x.OrderId);

                var orders = new List<OrderInfoFull>();
                foreach (var g in a)
                {
                    var ori = new OrderInfoFull()
                    {
                        Id = g.Key,
                        UserId = g.First().UserId,
                        UserName = g.First().UserName,
                        Products = new List<Product>()
                    };
                    foreach (var item in g)
                    {
                        ori.Products.Add(new Product()
                        {
                            Id = item.ProductId,
                            Name = item.ProductName,
                            Description = item.Description,
                            Price = item.Price,
                        });
                    }
                    ori.Products = ori.Products.OrderBy(x => x.Price).ToList();
                    orders.Add(ori);
                }

                return orders;

            }
        }
    }
}
