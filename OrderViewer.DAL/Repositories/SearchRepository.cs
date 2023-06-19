using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private ApplicationDBContext _db;

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
                //var users =
                //    (IEnumerable<UserData>)(from u in _db.UserData
                //                            select new
                //                            {
                //                                Id = u.Id,
                //                                Name = u.Name
                //                            }).ToList();
                var users = new List<UserData>();
                users.AddRange(_db.UserData.Select(x=>new UserData()
                {
                    Id = x.Id,
                    Name = x.Name
                }
                    ));
                //foreach (var user in _db.UserData)
                //{
                //    users.Add(new UserData
                //    {
                //        Id = user.Id,
                //        Name = user.Name
                //    });
                //}
                
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
