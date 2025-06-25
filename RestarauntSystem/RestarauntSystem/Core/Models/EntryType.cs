using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    [Table("entry_types")]
    public class EntryType
    {
        [Key]
        public int EntryTypeId { get; set; }
        public string EntryTypeName { get; set; }

        public ICollection<FeedbackEntry> FeedbackEntries { get; set; }
    }
}
