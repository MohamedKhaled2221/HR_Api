using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.SalaryReport.Dtos;

namespace HR.Application.Services
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryReportDto>> GetAllAsync(SalaryFilterDto filter);
        Task<SalaryReportDto?> GetByEmployeeAsync(SalaryFilterDto filter);
    }
}
