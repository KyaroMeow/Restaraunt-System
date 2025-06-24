using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class DishCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
