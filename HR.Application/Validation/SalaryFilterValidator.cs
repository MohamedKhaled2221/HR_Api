using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.SalaryReport.Dtos;

namespace HR.Application.Validation
{
    public class SalaryFilterValidator : AbstractValidator<SalaryFilterDto>
    {
        public SalaryFilterValidator()
        {
            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12).WithMessage("من فضلك اختر شهر صحيح");

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(2008).WithMessage("من فضلك اختر سنة صحيحة")
                .LessThanOrEqualTo(DateTime.Today.Year).WithMessage("من فضلك اختر سنة صحيحة");
        }
    }
}

