using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Inventory
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Quantity { get; set; }
    }
}
