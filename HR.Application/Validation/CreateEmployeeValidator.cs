using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.Employees.DTOs;

namespace HR.Application.Validation
{
    #region Employee Validator
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Matches(@"^\d{11}$").WithMessage("من فضلك ادخل رقم تليفون صحيح");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidAge).WithMessage("يجب ألا يقل عمر الموظف عن 20 سنة");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(14).WithMessage("يجب ألا يقل الرقم القومى عن 14 رقم!")
                .Matches(@"^\d+$").WithMessage("يجب ألا يقل الرقم القومى عن 14 رقم!");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");


            RuleFor(x => x.ContractDate)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidContractDate).WithMessage("من فضلك ادخل تاريخ تعاقد صحيح");

            RuleFor(x => x.BasicSalary)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .GreaterThan(0).WithMessage("من فضلك ادخل راتب صحيح");

            RuleFor(x => x.CheckInTime)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.CheckOutTime)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must((dto, checkOut) => checkOut > dto.CheckInTime)
                .WithMessage("موعد الانصراف يجب أن يكون بعد موعد الحضور");
        }



        private bool BeValidAge(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 20;
        }

        private bool BeValidContractDate(DateTime contractDate)
        {

            return contractDate >= new DateTime(2008, 1, 1)
                && contractDate <= DateTime.Today;
        }
    }

    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Matches(@"^\d{11}$").WithMessage("من فضلك ادخل رقم تليفون صحيح");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidAge).WithMessage("يجب ألا يقل عمر الموظف عن 20 سنة");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(14).WithMessage("يجب ألا يقل الرقم القومى عن 14 رقم!")
                .Matches(@"^\d+$").WithMessage("يجب ألا يقل الرقم القومى عن 14 رقم!");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.ContractDate)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must(BeValidContractDate).WithMessage("من فضلك ادخل تاريخ تعاقد صحيح");

            RuleFor(x => x.BasicSalary)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .GreaterThan(0).WithMessage("من فضلك ادخل راتب صحيح");

            RuleFor(x => x.CheckInTime)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.CheckOutTime)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .Must((dto, checkOut) => checkOut > dto.CheckInTime)
                .WithMessage("موعد الانصراف يجب أن يكون بعد موعد الحضور");
        }

        private bool BeValidAge(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 20;
        }

        private bool BeValidContractDate(DateTime contractDate)
        {
            return contractDate >= new DateTime(2008, 1, 1)
                && contractDate <= DateTime.Today;
        }
    } 
    #endregion
}
 