using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public decimal? OvertimeHours { get; set; }
        public decimal? DeductionHours { get; set; }

        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;

        // ── Meta ──────────────────────────────────────────
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ── Navigation ────────────────────────────────────
        public Employee Employee { get; set; } = null!;
    }
    public enum AttendanceStatus
    {
        Present = 1,   // حاضر
        Absent = 2,   // غائب
        Holiday = 3,   // إجازة رسمية
        Weekend = 4    // إجازة أسبوعية
    }
}
