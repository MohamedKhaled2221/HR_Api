using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.Application.Identity;

namespace HR.Application.Validation
{

    #region Auth Validator
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty().WithMessage("من فضلك ادخل اسم مستخدم صالح");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("من فضلك ادخل كلمة مرور صالحة");
        }
    }


    public class CreateUserGroupValidator : AbstractValidator<CreateUserGroupDto>
    {
        public CreateUserGroupValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("من فضلك ادخل اسم المجموعة")
                .MaximumLength(100).WithMessage("اسم المجموعة طويل جداً");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("من فضلك قم بتحديد صلاحيات المجموعة قبل الاضافة");
        }
    }

    public class UpdateUserGroupValidator : AbstractValidator<UpdateUserGroupDto>
    {
        public UpdateUserGroupValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("من فضلك ادخل اسم المجموعة")
                .MaximumLength(100).WithMessage("اسم المجموعة طويل جداً");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("من فضلك قم بتحديد صلاحيات المجموعة قبل الاضافة");
        }
    }


    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MaximumLength(100);

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(3).WithMessage("اسم المستخدم لا يقل عن 3 حروف")
                .MaximumLength(50)
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("اسم المستخدم يحتوي على أحرف غير صالحة");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .EmailAddress().WithMessage("من فضلك ادخل بريد الكتروني صالح");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(6).WithMessage("كلمة المرور لا تقل عن 6 أحرف");

            RuleFor(x => x.UserGroupId)
                .GreaterThan(0).WithMessage("من فضلك قم بتحديد صلاحيات المجموعة قبل الاضافة");
        }
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MaximumLength(100);

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(3).WithMessage("اسم المستخدم لا يقل عن 3 حروف")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("اسم المستخدم يحتوي على أحرف غير صالحة");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .EmailAddress().WithMessage("من فضلك ادخل بريد الكتروني صالح");

            RuleFor(x => x.UserGroupId)
                .GreaterThan(0).WithMessage("من فضلك قم بتحديد صلاحيات المجموعة قبل الاضافة");
        }
    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(6).WithMessage("كلمة المرور لا تقل عن 6 أحرف");
        }
    } 
    #endregion
}
