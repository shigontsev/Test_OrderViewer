using OrderViewer.Common.Entities;

namespace OrderViewer.Service.Interfaces
{
    public interface ISearchService
    {
        UserData GetUser(int id);

        UserData GetUser(string username);

        IEnumerable<UserData> GetAllUser();

        Product GetProduct(int id);

        Product GetProduct(string name);

        IEnumerable<Product> GetAllProduct();

        IEnumerable<Product> GetProductsBySubName(string name);

        IEnumerable<UserData> GetUsersBySubName(string name);

        IEnumerable<OrderInfoFull> FiltrUserProductBySubName(string user_subName, string product_subName);

        IEnumerable<OrderInfoFull> FiltrUserProductById(int user_id, int product_id);
    }
}
