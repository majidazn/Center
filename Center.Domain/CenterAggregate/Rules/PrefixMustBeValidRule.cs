using System.Text.RegularExpressions;
using Framework.DomainDrivenDesign.Domain.SeedWork;

namespace Center.Domain.CenterAggregate.Rules
{
    public class PrefixMustBeValidRule : IBusinessRule
    {
        public readonly string _prefix;
        public PrefixMustBeValidRule(string prefix)
        {
            _prefix = prefix;
        }
        public bool IsBroken()
        {
            return !Regex.IsMatch(_prefix, @"^0\d{2,3}");
        }

        public string Message => "پیش شماره وارد شده صحیح نمی باشد";
    }
}
