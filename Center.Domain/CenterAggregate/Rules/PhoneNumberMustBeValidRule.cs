using System.Text.RegularExpressions;
using Framework.DomainDrivenDesign.Domain.SeedWork;

namespace Center.Domain.CenterAggregate.Rules
{
    public class PhoneNumberMustBeValidRule : IBusinessRule
    {
        public readonly string _phoneNumber;

        public PhoneNumberMustBeValidRule(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }
        public bool IsBroken()
        {
            //return Regex.IsMatch(_phoneNumber, "(^(09|9)[1-9][0-9]\\d{7}$)");
            return !Regex.IsMatch(_phoneNumber, "09(1[0-9]|3[0-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}");
        }

        public string Message => "شماره موبایل وارد شده صحیح نمی باشد";
    }
}
