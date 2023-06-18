using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int? UserDataId { get; set; }

        public int? ProductId { get; set; }
    }
}
