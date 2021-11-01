using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.AccountService
{
    public interface IAccountServices
    {
        int GetHighestRoles(IHttpContextAccessor httpContextAccessor);
        Task<List<string>> GetRawDynamicPermissions(List<int> roleIds);
    }
}
