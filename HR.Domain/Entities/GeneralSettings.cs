using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Entities
{
    public class GeneralsSettings
    {
        public int Id { get; set; }

        // ── Overtime & Deduction 
        public decimal OvertimeHourValue { get; set; }

        public decimal DeductionHourValue { get; set; }

        // ── Weekly Holidays 
        public string WeeklyHolidayDay1 { get; set; } = string.Empty;

        public string? WeeklyHolidayDay2 { get; set; }

      
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
