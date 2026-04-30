using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.Attendance;

namespace HR.Application.Validation
{
    public class CreateAttendanceValidator : AbstractValidator<CreateAttendanceDto>
    {
        public CreateAttendanceValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(d => d <= DateTime.Today).WithMessage("من فضلك ادخل تاريخ صحيح")
                .Must(d => d.Year >= 2008).WithMessage("من فضلك ادخل تاريخ صحيح");

           
            RuleFor(x => x)
                .Must(x => x.CheckOutTime == null ||
                           x.CheckInTime == null ||
                           x.CheckOutTime > x.CheckInTime)
                .WithMessage("موعد الانصراف يجب أن يكون بعد موعد الحضور");
        }
    }

    public class UpdateAttendanceValidator : AbstractValidator<UpdateAttendanceDto>
    {
        public UpdateAttendanceValidator()
        {
            RuleFor(x => x)
                .Must(x => x.CheckOutTime == null ||
                           x.CheckInTime == null ||
                           x.CheckOutTime > x.CheckInTime)
                .WithMessage("موعد الانصراف يجب أن يكون بعد موعد الحضور");
        }
    }

    public class AttendanceFilterValidator : AbstractValidator<AttendanceFilterDto>
    {
        public AttendanceFilterValidator()
        {
    
            RuleFor(x => x)
                .Must(x => x.DateFrom == null ||
                           x.DateTo == null ||
                           x.DateFrom <= x.DateTo)
                .WithMessage("من فضلك ادخل تاريخ صحيح");
        }
    }
}
