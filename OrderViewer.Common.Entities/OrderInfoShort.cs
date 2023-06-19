using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.Common.Entities
{
    public class OrderInfoShort
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
