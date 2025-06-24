using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; }
        public Position Position { get; set; }

        [ForeignKey("WorkSchedule")]
        public int? ScheduleId { get; set; }
        public WorkSchedule WorkSchedule { get; set; }

        [ForeignKey("EmployeeStatus")]
        public int? StatusId { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
