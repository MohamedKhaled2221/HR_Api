namespace HR.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        // ── Personal Info 
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;

        // ── Work Info 
        public DateTime ContractDate { get; set; }
        public decimal BasicSalary { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }

        // ── Meta 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
     
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}
