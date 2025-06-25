using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
