using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class OrderItem
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int Quantity { get; set; }
    }
}
