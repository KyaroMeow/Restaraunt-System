using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public Inventory Inventory { get; set; }
        public ICollection<DishComponent> DishComponents { get; set; }
        public ICollection<DeliveryItem> DeliveryItems { get; set; }
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}
