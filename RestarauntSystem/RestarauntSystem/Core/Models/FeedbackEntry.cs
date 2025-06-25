using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RestarauntSystem.Core.Models
{
    [Table("feedback_entry")]
    public class FeedbackEntry
    {
        [Key]
        public int EntryId { get; set; }
        public int EntryTypeId { get; set; }
        public string CustomerName { get; set; }
        public string ContactInfo { get; set; }
        public string EntryText { get; set; }

        public EntryType EntryType { get; set; }
    }
}
