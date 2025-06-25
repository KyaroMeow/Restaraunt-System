using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("zones")]
    public class Zone
    {
        [Key]
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }

        public ICollection<Table> Tables { get; set; }
    }
}
