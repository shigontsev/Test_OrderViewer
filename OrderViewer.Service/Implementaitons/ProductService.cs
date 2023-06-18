using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;
using OrderViewer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.Service.Implementaitons
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) 
        { 
            _productRepository = productRepository;
        }

        public bool AddProduct(Product product)
        {
            return _productRepository.AddProduct(product);
        }

        public bool DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public bool DeleteProduct(Product product)
        {
            return _productRepository.DeleteProduct(product);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _productRepository.GetAllProduct();
        }

        public Product GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }

        public Product GetProduct(string name)
        {
            return _productRepository.GetProduct(name);
        }

        public bool UpdateProduct(Product product)
        {
            return _productRepository.UpdateProduct(product);
        }
    }
}
