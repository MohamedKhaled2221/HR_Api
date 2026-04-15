using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.GeneralSettings.Dtos
{
    public class UpdateGeneralSettingsDto
    {
        public decimal OvertimeHourValue { get; set; }
        public decimal DeductionHourValue { get; set; }
        public string WeeklyHolidayDay1 { get; set; } = string.Empty;
        public string? WeeklyHolidayDay2 { get; set; }
    }
}
