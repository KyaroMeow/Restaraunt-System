using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        public Zone Zone { get; set; }

        [ForeignKey("Status")]
        public int? StatusId { get; set; }
        public TableStatus Status { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
