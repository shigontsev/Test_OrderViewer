using OrderViewer.Common.Entities;

namespace OrderViewer.DAL.Interfaces
{
    public interface IUserOrderRepository : IUserRepository, IOrderRepository
    {
        OrderInfoFull GetOrderById(int order_id);

        IEnumerable<OrderInfoShort> GetAllOrdersShort();

        IEnumerable<OrderInfoFull> GetAllOrdersFull();

        IEnumerable<OrderInfoShort> GetAllOrdersShortByUserId(int user_id);

        IEnumerable<OrderInfoFull> GetAllOrdersFullByUserId(int user_id);
    }
}
