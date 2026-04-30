using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.SalaryReport.Dtos
{
    public class SalaryFilterDto
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
