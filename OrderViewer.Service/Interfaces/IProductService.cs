using OrderViewer.Common.Entities;

namespace OrderViewer.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct();

        Product GetProduct(int id);

        Product GetProduct(string name);

        bool AddProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int id);

        bool DeleteProduct(Product product);
    }
}
