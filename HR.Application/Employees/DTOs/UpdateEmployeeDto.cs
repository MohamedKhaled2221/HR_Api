using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Employees.DTOs
{
    public class UpdateEmployeeDto
    {
        // Personal Info
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;

        // Work Info
        public DateTime ContractDate { get; set; }
        public decimal BasicSalary { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
    }
}
