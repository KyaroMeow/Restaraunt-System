using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("dish_categories")]
    public class DishCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
