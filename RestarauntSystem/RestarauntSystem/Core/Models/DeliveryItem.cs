using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class DeliveryItem
    {
        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public decimal Quantity { get; set; }
    }
}
