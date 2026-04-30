using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Attendance
{
  
    public class AttendanceFilterDto
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
