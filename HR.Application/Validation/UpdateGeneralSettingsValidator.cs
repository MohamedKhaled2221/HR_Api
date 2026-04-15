using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.GeneralSettings.Dtos;

namespace HR.Application.Validation
{

    #region GeneralSettings Validator
    public class UpdateGeneralSettingsValidator : AbstractValidator<UpdateGeneralSettingsDto>
    {
        private static readonly string[] ValidDays =
        {
            "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"
        };

        public UpdateGeneralSettingsValidator()
        {
            RuleFor(x => x.OvertimeHourValue)
                .GreaterThan(0).WithMessage("من فضلك ادخل بيانات الحقل")
                .LessThanOrEqualTo(24).WithMessage("قيمة الإضافة غير صحيحة");

            RuleFor(x => x.DeductionHourValue)
                .GreaterThan(0).WithMessage("من فضلك ادخل بيانات الحقل")
                .LessThanOrEqualTo(24).WithMessage("قيمة الخصم غير صحيحة");

            RuleFor(x => x.WeeklyHolidayDay1)
                .NotEmpty().WithMessage("من فضلك ادخل بيانات الحقل")
                .Must(d => ValidDays.Contains(d)).WithMessage("من فضلك اختر يوم إجازة صحيح");


            RuleFor(x => x.WeeklyHolidayDay2)
                .Must(d => ValidDays.Contains(d)).WithMessage("من فضلك اختر يوم إجازة صحيح")
                .When(x => !string.IsNullOrEmpty(x.WeeklyHolidayDay2));

            RuleFor(x => x)
                .Must(x => x.WeeklyHolidayDay2 != x.WeeklyHolidayDay1)
                .WithMessage("يوم الإجازة الثاني يجب أن يكون مختلفاً عن الأول")
                .When(x => !string.IsNullOrEmpty(x.WeeklyHolidayDay2));
        }
    } 
    #endregion
}

