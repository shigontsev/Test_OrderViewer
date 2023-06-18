using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDBContext _db;
        //public ProductRepository() 
        //{ 
        //    _db = new ApplicationDBContext();
        //}
        public IEnumerable<Product> GetAllProduct()
        {
            using (_db = new ApplicationDBContext())
            {
                return _db.Product.ToArray();
            }
        }

        public bool AddProduct(Product product)
        {
            if (product == null)
            {
                return false;
                //throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return false;
            }
            if(GetProduct(product.Name) != null)
            {
                return false;
            }

            using (_db = new ApplicationDBContext())
            {
                _db.Product.Add(new Product()
                {
                    Name = product.Name.Trim(),
                    Description = product.Description.Trim(),
                });
                _db.SaveChanges();
            }

            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null)
            {
                return false;
                //throw new ArgumentNullException();
            }
            using (_db = new ApplicationDBContext())
            {
                _db.Product.Remove(product);
                _db.SaveChanges();
            }

            return true;
        }

        public bool DeleteProduct(Product product)
        {
            return DeleteProduct(product.Id);
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
                };
            }
        }

        public bool UpdateProduct(Product product)
        {
            var product_old = GetProduct(product.Id);

            if (product_old == null) { 
                return false;
            }
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return false;
            }

            //product_old.Name = product.Name;
            //product_old.Description = product.Description;
            using (_db = new ApplicationDBContext())
            {
                _db.Product.Update(product);
                _db.SaveChanges();
            }

            return true;
        }
    }
}
