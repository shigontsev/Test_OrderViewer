using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;

namespace OrderViewer.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private ApplicationDBContext _db;

        public IEnumerable<OrderInfoFull> FiltrUserProductById(int user_id, int product_id)
        {
            using (_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where (user.Id == user_id && p.Id == product_id)
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

        public IEnumerable<OrderInfoFull> FiltrUserProductBySubName(string user_subName, string product_subName)
        {
            using(_db = new ApplicationDBContext())
            {
                var a = (from user in _db.UserData
                         join order in _db.Order on user.Id equals order.UserDataId
                         join op in _db.OrderProduct on order.Id equals op.OrderId
                         join p in _db.Product on op.ProductId equals p.Id
                         where (user.Name.Contains(user_subName) && p.Name.Contains(product_subName))
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

        public IEnumerable<Product> GetAllProduct()
        {
            using (_db = new ApplicationDBContext())
            {
                return _db.Product.ToList();
            }
        }

        public IEnumerable<UserData> GetAllUser()
        {
            using (_db = new ApplicationDBContext())
            {
                var users = new List<UserData>();
                users.AddRange(_db.UserData.Select(x => new UserData()
                {
                    Id = x.Id,
                    Name = x.Name
                }
                    ));

                return users.ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (_db = new ApplicationDBContext())
            {
                var product = _db.Product.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return null;
                }
                return new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };
            }
        }

        public Product GetProduct(string name)
        {
            name = name.Trim();
            using (_db = new ApplicationDBContext())
            {
                var product = _db.Product.FirstOrDefault(x => x.Name == name);
                if (product == null)
                {
                    return null;
                }
                return new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };
            }
        }

        public IEnumerable<Product> GetProductsBySubName(string name)
        {
            using(_db = new ApplicationDBContext())
            {
                var products = _db.Product.Where(x => x.Name.Contains(name)).ToList();

                return products;
            }
        }

        public UserData GetUser(int id)
        {
            using (_db = new ApplicationDBContext())
            {
                UserData user = _db.UserData.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return null;
                }
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

                if (user == null)
                {
                    return null;
                }
                
                return new UserData()
                {
                    Id = user.Id,
                    Name = user.Name
                };
            }
        }

        public IEnumerable<UserData> GetUsersBySubName(string name)
        {
            using (_db = new ApplicationDBContext())
            {
                var users = _db.UserData.Where(x => x.Name.Contains(name))
                    .Select(p => new UserData()
                    {
                        Id = p.Id,
                        Name = p.Name,

                    }).ToList();

                return users;
            }
        }
    }
}
