﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("deliveries")]
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Employee Employee { get; set; }
        public ICollection<DeliveryItem> DeliveryItems { get; set; }
    }
}
