using OrderViewer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Interfaces
{
    public interface IOrderRepository
    {
        bool CreateOrder(int user_id, IEnumerable<Product> products);

        bool CreateOrder(string user_name, IEnumerable<Product> products);
    }
}
