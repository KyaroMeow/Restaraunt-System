using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

}
