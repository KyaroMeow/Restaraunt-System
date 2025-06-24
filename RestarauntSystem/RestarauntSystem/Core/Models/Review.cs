using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int? CustomerId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
    }
}
