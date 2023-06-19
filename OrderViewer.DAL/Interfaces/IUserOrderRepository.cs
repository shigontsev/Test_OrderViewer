using OrderViewer.Common.Entities;

namespace OrderViewer.DAL.Interfaces
{
    public interface IUserOrderRepository : IUserRepository, IOrderRepository
    {
        IEnumerable<OrderInfoShort> GetAllOrdersShort();

        IEnumerable<OrderInfoFull> GetAllOrdersFull();

        IEnumerable<OrderInfoShort> GetAllOrdersShortByUserId(int user_id);

        IEnumerable<OrderInfoFull> GetAllOrdersFullByUserId(int user_id);
    }
}
