using System.Text.RegularExpressions;
using Framework.DomainDrivenDesign.Domain.SeedWork;


namespace Center.Domain.CenterAggregate.Rules
{
    public class InstagramUserNameMustBeValidRule : IBusinessRule
    {
        private readonly string _instagramUserName;

        public InstagramUserNameMustBeValidRule(string instagramUserName)
        {
            this._instagramUserName = instagramUserName;
        }
        public bool IsBroken()
        {
            return Regex.IsMatch(_instagramUserName, @"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$");
        }

        public string Message => "آی دی اینستاگرام وارد شده صحیح نمی باشد";
    }
}
