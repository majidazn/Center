using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.EditCenterVariableCommand
{
    public class EditCenterVariableCommandValidator : AbstractValidator<EditCenterVariableCommand>
    {
        public EditCenterVariableCommandValidator()
        {
            RuleFor(i => i.EditCenterVariableDto.Name).NotEmpty().NotNull().WithMessage("نام اجباری می باشد");
            RuleFor(i => i.EditCenterVariableDto.EnName).NotEmpty().NotNull().WithMessage("نام انگلیسی اجباری می باشد");
            RuleFor(i => i.EditCenterVariableDto.Name).MaximumLength(150).WithMessage("نام حداکثر 150 کاراکتر می باشد");
            RuleFor(i => i.EditCenterVariableDto.EnName).MaximumLength(80).WithMessage("نام حداکثر 150 کاراکتر می باشد");


            RuleFor(i => i.EditCenterVariableDto.ParentId).GreaterThan(0).WithMessage("مقدار parentId  اجباری می باشدس");
        }
    }
}
