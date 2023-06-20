using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;

namespace OrderViewer.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDBContext _db;

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
                    Price = product.Price,
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

            using (_db = new ApplicationDBContext())
            {
                _db.Product.Update(product);
                _db.SaveChanges();
            }

            return true;
        }
    }
}
