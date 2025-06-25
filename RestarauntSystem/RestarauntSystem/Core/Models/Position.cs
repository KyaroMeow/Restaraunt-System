using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("positions")]
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
