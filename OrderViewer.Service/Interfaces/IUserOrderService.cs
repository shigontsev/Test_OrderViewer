using OrderViewer.Common.Entities;

namespace OrderViewer.Service.Interfaces
{
    public interface IUserOrderService
    {
        bool CreateUser(string login, string password);

        bool IsAuthentication(string login, string password);

        UserData GetUser(int id);

        UserData GetUser(string username);

        IEnumerable<UserData> GetUsers();

        bool CreateOrder(int user_id, IEnumerable<Product> products);

        bool CreateOrder(string user_name, IEnumerable<Product> products);

        OrderInfoFull GetOrderById(int order_id);

        IEnumerable<OrderInfoShort> GetAllOrdersShort();

        IEnumerable<OrderInfoFull> GetAllOrdersFull();

        IEnumerable<OrderInfoShort> GetAllOrdersShortByUserId(int user_id);

        IEnumerable<OrderInfoFull> GetAllOrdersFullByUserId(int user_id);
    }
}
