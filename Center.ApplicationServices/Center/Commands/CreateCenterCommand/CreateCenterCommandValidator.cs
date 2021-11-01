using Center.Domain.CenterAggregate.Dtos.CenterTelecom;
using Center.Domain.CenterAggregate.Dtos.ElectronicAddress;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.CreateCenterCommand
{
    public class CreateCenterCommandValidator : AbstractValidator<CreateCenterCommand>
    {
        public CreateCenterCommandValidator()
        {
            RuleFor(i => i.CreateCenterDto.Telecoms).NotNull().WithMessage("حداقل یک شماره موبایل اجباری می باشد");



            RuleFor(i => i.CreateCenterDto.Name).NotEmpty().NotNull().WithMessage("نام اجباری می باشد");
            RuleFor(i => i.CreateCenterDto.Name).MaximumLength(300).WithMessage("نام حداکثر 300 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.Logo).Must(CheckLogo).WithMessage("لوگو نباید از 200 کیلو بایت بیشتر باشد");



            RuleFor(i => i.CreateCenterDto.Title).GreaterThan(0).WithMessage("مقدار عنوان مرکز اجباری است");
            RuleFor(i => i.CreateCenterDto.City).GreaterThan(0).WithMessage("مقدار شهر اجباری است");
            RuleFor(i => i.CreateCenterDto.CenterGroup).GreaterThan(0).WithMessage("مقدار گروه مرکز اجباری است");
            RuleFor(i => i.CreateCenterDto.HostName).MaximumLength(200).WithMessage("آدرس اینترنتی حداکثر 200 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.NationalCode).MaximumLength(20).WithMessage("کد ملی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.FinanchialCode).MaximumLength(20).WithMessage("کد اقتصادی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.SepasCode).MaximumLength(20).WithMessage("کد سپاس مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.ZipCode).MaximumLength(20).WithMessage("کد پستی مرکز حداکثر 20 کاراکتر می باشد");
            RuleFor(i => i.CreateCenterDto.ValidFrom).NotNull().NotEmpty().WithMessage("تاریخ شروع اجباری است");



            RuleFor(i => i.CreateCenterDto.EnName).MaximumLength(300).WithMessage("نام انگلیسی حداکثر 200 کاراکتر می باشد");



            RuleForEach(i => i.CreateCenterDto.Telecoms).SetValidator(new TelecomValidator());
            RuleForEach(i => i.CreateCenterDto.ElectronicAddresses).SetValidator(new ElectronicAddressValidator());
        }
        private bool CheckLogo(byte[] logo)
        {
            if (logo != null && (logo.Length / 1024) > 200)
                return false;
            return true;
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
