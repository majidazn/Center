using Center.Domain.CenterAggregate.Dtos.CenterTelecom;
using Center.Domain.CenterAggregate.Dtos.ElectronicAddress;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.EditCenterCommand
{
    public class EditCenterCommandValidator : AbstractValidator<EditCenterCommand>
    {
        public EditCenterCommandValidator()
        {
            RuleFor(i => i.EditCenterDto.Telecoms).NotNull().WithMessage("حداقل یک شماره موبایل اجباری می باشد");



            RuleFor(i => i.EditCenterDto.Name).NotEmpty().NotNull().WithMessage("نام اجباری می باشد");
            RuleFor(i => i.EditCenterDto.Name).MaximumLength(300).WithMessage("نام حداکثر 300 کاراکتر می باشد");



            RuleFor(i => i.EditCenterDto.Title).GreaterThan(0).WithMessage("مقدار عنوان مرکز اجباری است");
            RuleFor(i => i.EditCenterDto.CenterGroup).GreaterThan(0).WithMessage("مقدار گروه مرکز اجباری است");
            RuleFor(i => i.EditCenterDto.HostName).MaximumLength(200).WithMessage("آدرس اینترنتی حداکثر 200 کاراکتر می باشد");
            RuleFor(i => i.EditCenterDto.NationalCode).MaximumLength(20).WithMessage("کد ملی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.EditCenterDto.FinanchialCode).MaximumLength(20).WithMessage("کد اقتصادی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.EditCenterDto.SepasCode).MaximumLength(20).WithMessage("کد سپاس مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.EditCenterDto.ZipCode).MaximumLength(20).WithMessage("کد پستی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.EditCenterDto.ValidFrom).NotNull().NotEmpty().WithMessage("تاریخ شروع اجباری است");



            RuleFor(i => i.EditCenterDto.EnName).MaximumLength(300).WithMessage("نام انگلیسی حداکثر 200 کاراکتر می باشد");



            RuleForEach(i => i.EditCenterDto.Telecoms).SetValidator(new TelecomValidator());
            RuleForEach(i => i.EditCenterDto.ElectronicAddresses).SetValidator(new ElectronicAddressValidator());
        }

        public class TelecomValidator : AbstractValidator<CreateTelecomDto>
        {
            public TelecomValidator()
            {
                RuleFor(telecom => telecom.TellNo).NotEmpty().NotNull().WithMessage("شماره موبایل/تلفن اجباری می باشد");
                RuleFor(telecom => telecom.TellNo).MaximumLength(15).WithMessage("شماره موبایل/تلفن حداکثر 15 کاراکتر می باشد");
                RuleFor(telecom => telecom.Type).GreaterThan(0).WithMessage("نوع تلفن اجباری می باشد");
            }
        }

        public class ElectronicAddressValidator : AbstractValidator<CreateElectronicAddressDto>
        {
            public ElectronicAddressValidator()
            {
                RuleFor(telecom => telecom.EAddress).NotEmpty().NotNull().WithMessage("آدرس اجباری می باشد");
                RuleFor(telecom => telecom.EAddress).MaximumLength(200).WithMessage("آدرس حداکثر 200 کاراکتر می باشد");
                RuleFor(telecom => telecom.EType).GreaterThan(0).WithMessage("نوع آدرس اجباری می باشد");
            }
        }
    }
}
