using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("dishes")]
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }

        public DishCategory DishCategory { get; set; }
        public ICollection<DishComponent> DishComponents { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
