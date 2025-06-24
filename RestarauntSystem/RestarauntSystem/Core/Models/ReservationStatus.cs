using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class ReservationStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
