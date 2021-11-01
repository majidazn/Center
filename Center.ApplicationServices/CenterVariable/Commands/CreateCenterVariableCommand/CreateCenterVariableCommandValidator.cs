using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.CreateCenterVariableCommand
{
    public class CreateCenterVariableCommandValidator : AbstractValidator<CreateCenterVariableCommand>
    {
        public CreateCenterVariableCommandValidator()
        {

            RuleFor(i => i.CreateCenterVariableDto.Name).NotEmpty().NotNull().WithMessage("نام اجباری می باشد");
            RuleFor(i => i.CreateCenterVariableDto.EnName).NotEmpty().NotNull().WithMessage("نام انگلیسی اجباری می باشد");
            RuleFor(i => i.CreateCenterVariableDto.Name).MaximumLength(150).WithMessage("نام حداکثر 150 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterVariableDto.EnName).MaximumLength(80).WithMessage("نام حداکثر 150 کاراکتر می باشد");

            RuleFor(i => i.CreateCenterVariableDto.Address).MaximumLength(150).WithMessage("آدرس حداکثر 150 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterVariableDto.ShortKey).MaximumLength(30).WithMessage("میانبر حداکثر 30 کاراکتر می باشد");

            RuleFor(i => i.CreateCenterVariableDto.ParentId).GreaterThan(0).WithMessage("مقدار parentId  اجباری می باشدس");

            RuleFor(i => i.CreateCenterVariableDto.Icon).Must(CheckIcon).WithMessage("آیکن نباید از 200 کیلو بایت بیشتر باشد");
        }

        private bool CheckIcon(byte[]? icon)
        {
            if (icon != null && (icon.Length / 1024) > 200)
                return false;

            return true;
        }
    }
}
