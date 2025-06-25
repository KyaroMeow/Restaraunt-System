using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class EntryType
    {
        [Key]
        public int EntryTypeId { get; set; }
        public string EntryTypeName { get; set; }

        public ICollection<FeedbackEntry> FeedbackEntries { get; set; }
    }
}
