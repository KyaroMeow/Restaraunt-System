using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class SupplierProduct
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public decimal UsualPrice { get; set; }

        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
    }
}
