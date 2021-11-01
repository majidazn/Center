using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Commands.CreateActivityCommand
{
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(x => x.ActivityDto.ValidFrom).NotNull().WithMessage("تاریخ شروع اجباری می باشد");
            RuleFor(x => x.ActivityDto.CenterVariableId).GreaterThan(0).WithMessage("برنامه مورد نظر برای تخصیص به مرکز انتخاب نشده است");
        }
    }
}
