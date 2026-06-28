using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;

namespace HR.Domain.Repositories
{
    public interface ISalaryRepository
    {
   
        Task<IEnumerable<Attendance>> GetAttendanceByMonthAsync(int employeeId, int month, int year);

   
        Task<IEnumerable<Employee>> GetAllEmployeesWithAttendanceAsync(int month, int year);

        Task<Employee?> GetEmployeeWithAttendanceAsync(int employeeId, int month, int year);
    }
}
