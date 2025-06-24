using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class WorkSchedule
    {
        public int ScheduleId { get; set; }
        public string ScheduleType { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
