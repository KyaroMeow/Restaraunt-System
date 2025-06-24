using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("Table")]
        public int? TableId { get; set; }
        public Table Table { get; set; }

        [ForeignKey("Status")]
        public int? StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime OrderTime { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
