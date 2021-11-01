using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Extensions;

namespace Center.Domain.CenterAggregate.Rules
{
    public class EmailAddressMustBeValidRule : IBusinessRule
    {
        private readonly string _emailAddress;

        public EmailAddressMustBeValidRule(string emailAddress)
        {
            this._emailAddress = emailAddress;
        }
        public bool IsBroken() => !_emailAddress.IsValidEmail();
        public string Message => "آدرس ایمیل وارد شده صحیح نمی باشد";
    }
}
