using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Extensions;

namespace Center.Domain.CenterAggregate.Rules
{
    public class NationalCodeMustBeValidRule : IBusinessRule
    {
        private readonly string _value;

        public NationalCodeMustBeValidRule(string value)
        {
            this._value = value;
        }

        public string Message => "کد ملی وارد شده صحیح نمی باشد";

        public bool IsBroken() => !_value.IsValidNationalCode(); 
    }
}
