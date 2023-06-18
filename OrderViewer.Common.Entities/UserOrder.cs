using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.Common.Entities
{
    public class UserOrder
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public Decimal Price { get; set; }

    }
}
