using OrderViewer.Common.Entities;

namespace OrderViewer.DAL.Interfaces
{
    public interface IOrderRepository
    {
        bool CreateOrder(int user_id, IEnumerable<Product> products);

        bool CreateOrder(string user_name, IEnumerable<Product> products);
    }
}
