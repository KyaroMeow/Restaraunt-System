using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("suppliers")]
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public ICollection<DeliveryItem> DeliveryItems { get; set; }
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}
