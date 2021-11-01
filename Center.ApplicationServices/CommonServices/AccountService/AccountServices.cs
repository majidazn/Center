using Core.Common.Enums;
using Framework.Exceptions;
using Framework.Extensions;
using Framework.RemoteData;
using Framework.RemoteData.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.AccountService
{
    public class AccountServices : IAccountServices
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor
        public AccountServices(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        public int GetHighestRoles(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return 0;

            var userRoleList = httpContextAccessor.GetRoleList();

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.Admin))
                return (int)AccountTypeRoles.Admin;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.EPDAdmin))
                return (int)AccountTypeRoles.EPDAdmin;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.UniAdmin))
                return (int)AccountTypeRoles.UniAdmin;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.EPDUser))
                return (int)AccountTypeRoles.EPDUser;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.CenterAdmin))
                return (int)AccountTypeRoles.CenterAdmin;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.CentersAdminResponsible))
                return (int)AccountTypeRoles.CentersAdminResponsible;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.HumanResourceResponsible))
                return (int)AccountTypeRoles.HumanResourceResponsible;
            else
                return (int)AccountTypeRoles.CenterUser;
        }

        public async Task<List<string>> GetRawDynamicPermissions(List<int> roleIds)
        {
            var model = new PostClientRequestDto<List<int>>
            {
                ApiUrl = "WebServices/GetRawDynamicPermissionsAsync",
                ClientFactory = _httpClientFactory,
                ClientName = nameof(HttpClientNameType.SecurityWebApi),
                InputModel = roleIds
            };

            var permissions = await APIRequest.Post<List<int>, List<string>>
                            .PostDataAsync(model);

            if (permissions.IsSucceeded)
                return permissions.Output;

            throw new AppException();
        }
        #endregion
    }
}
