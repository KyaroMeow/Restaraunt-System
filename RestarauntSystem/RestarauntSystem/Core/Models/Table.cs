using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("tables")]
    public class Table
    {
        [Key]
        [Column("table_id")]
        public int TableId { get; set; }

        [ForeignKey("Zone")]
        [Column("zone_id")]
        public int? ZoneId { get; set; }
        public Zone Zone { get; set; }

        // Это статус СТОЛА (не резервации)
        [ForeignKey("TableStatus")]
        [Column("status_id")] // Связь с table_status
        public int? StatusId { get; set; }
        public TableStatus Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
