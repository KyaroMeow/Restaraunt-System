using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("reservation_status")]
    public class ReservationStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
