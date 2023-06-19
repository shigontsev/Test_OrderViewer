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
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public UserOrderService(IUserOrderRepository userOrderRepository) 
        {
            _userOrderRepository = userOrderRepository;
        }

        public bool CreateUser(string login, string password)
        {
            return _userOrderRepository.CreateUser(login, password);
        }

        public bool IsAuthentication(string login, string password)
        {
            return _userOrderRepository.IsAuthentication(login, password);
        }

        public UserData GetUser(int id)
        {
            return _userOrderRepository.GetUser(id);
        }

        public UserData GetUser(string username)
        {
            return _userOrderRepository.GetUser(username);
        }

        public IEnumerable<UserData> GetUsers()
        {
            return _userOrderRepository.GetUsers();
        }

        public bool CreateOrder(int user_id, IEnumerable<Product> products)
        {
            return _userOrderRepository.CreateOrder(user_id, products);
        }

        public bool CreateOrder(string user_name, IEnumerable<Product> products)
        {
            return _userOrderRepository.CreateOrder(user_name, products);
        }

        public IEnumerable<OrderInfoShort> GetAllOrdersShort()
        {
            return _userOrderRepository.GetAllOrdersShort();
        }

        public IEnumerable<OrderInfoFull> GetAllOrdersFull()
        {
            return _userOrderRepository.GetAllOrdersFull();
        }

        public IEnumerable<OrderInfoShort> GetAllOrdersShortByUserId(int user_id)
        {
            return _userOrderRepository.GetAllOrdersShortByUserId(user_id);
        }

        public IEnumerable<OrderInfoFull> GetAllOrdersFullByUserId(int user_id)
        {
            return _userOrderRepository.GetAllOrdersFullByUserId(user_id);
        }
    }
}
