namespace HR.Application.SalaryReport.Dtos
{
    public class SalaryReportDto
    {

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal DeductionHours { get; set; }
        public decimal TotalOvertime { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetSalary { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
    }
}
