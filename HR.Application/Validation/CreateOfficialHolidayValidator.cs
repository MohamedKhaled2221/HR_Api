using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.OfficialHoliday.Dtos;

namespace HR.Application.Validation
{
    #region OfficialHolidays Validator
    public class CreateOfficialHolidayValidator : AbstractValidator<CreateOfficialHolidayDto>
    {
        public CreateOfficialHolidayValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MaximumLength(100).WithMessage("اسم الإجازة طويل جداً");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidYear).WithMessage("من فضلك ادخل تاريخ صحيح");
        }

        private bool BeValidYear(DateTime date)
        {
            return date.Year >= 2008;
        }
    }
    public class UpdateOfficialHolidayValidator : AbstractValidator<UpdateOfficialHolidayDto>
    {
        public UpdateOfficialHolidayValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MaximumLength(100).WithMessage("اسم الإجازة طويل جداً");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidYear).WithMessage("من فضلك ادخل تاريخ صحيح");
        }

        private bool BeValidYear(DateTime date)
        {
            return date.Year >= 2008;
        }
    } 
    #endregion
}
