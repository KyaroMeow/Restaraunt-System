using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class DishComponent
    {
        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Quantity { get; set; }
    }
}
