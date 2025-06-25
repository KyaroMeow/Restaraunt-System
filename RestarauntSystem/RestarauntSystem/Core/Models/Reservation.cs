using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("reservations")]
    public class Reservation
    {
        [Key]
        [Column("reservation_id")]
        public int ReservationId { get; set; }
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("ReservationStatus")]
        [Column("status_id")] // Связь с reservation_status
        public int? StatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        [ForeignKey("Table")]
        [Column("table_id")]
        public int? TableId { get; set; }
        public Table Table { get; set; }
        [Column("reservation_time", TypeName = "timestamp with time zone")]
        public DateTime ReservationTime { get; set; }


    }
}
