using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? StatusId { get; set; }
        public int? TableId { get; set; }
        public DateTime ReservationTime { get; set; }

        public Customer Customer { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public Table Table { get; set; }
    }
}
