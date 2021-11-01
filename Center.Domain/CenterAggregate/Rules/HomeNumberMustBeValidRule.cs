using System.Text.RegularExpressions;
using Framework.DomainDrivenDesign.Domain.SeedWork;


namespace Center.Domain.CenterAggregate.Rules
{
    public class HomeNumberMustBeValidRule : IBusinessRule
    {
        public readonly string _homeNumber;

        public HomeNumberMustBeValidRule(string homeNumber)
        {
            _homeNumber = homeNumber;
        }

        public bool IsBroken()
        {
            return Regex.IsMatch(_homeNumber, @"\d{8}$");
        }

        public string Message => "شماره تلفن وارد شده صحیح نمی باشد";
    }
}
