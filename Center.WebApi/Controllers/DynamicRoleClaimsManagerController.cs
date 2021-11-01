using Center.ApplicationServices.CommonServices.AccountService;
using Core.Common.Enums;
using Core.Common.ViewModels.JWT;
using Framework.Attributes;
using Framework.Controller;
using Framework.DynamicPermissions;
using Framework.DynamicPermissions.Services.HashingService;
using Framework.DynamicPermissions.Services.MvcActionsDiscovery;
using Framework.Exceptions;
using Framework.Extensions;
using Framework.RemoteData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{
    [DisplayName("سطح دسترسی")]
    public class DynamicRoleClaimsManagerController : ApiBaseController
    {
        //private readonly string _securityAppUrl;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
        private readonly IHashingService _hashingService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountServices _accountServices;

        public DynamicRoleClaimsManagerController(
            IMvcActionsDiscoveryService mvcActionsDiscoveryService,
            IHashingService hashingService,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IAccountServices accountServices
            )
        {
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _hashingService = hashingService;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _accountServices = accountServices;
        }

        [HttpGet]
        [DisplayName("مشاهده نقش ها")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<SelectListItem>> GetRoles()
        {

            int applicationId = (int)Applications.Center;
            var allRoles = new List<Role>();
            allRoles = await this.GetAllRolesAsync(applicationId).ConfigureAwait(false);

            var rolesList = allRoles.Select(x => new SelectListItem { Text = x.Description, Value = x.Id.ToString() }).ToList();

            return rolesList;
        }

        private async Task<Role> GetRoleByIdInTenant(int roleId)
        {
            var currentUserTenantId = _httpContextAccessor.HttpContext.User.Identity.GetUserTenantId();
            if (currentUserTenantId == null || currentUserTenantId == "0")
                throw new AppException();

            var role = await HttpClientFactoryRequest.Get<Role>
                .GetDataByClientNameAsync(_httpClientFactory, HttpClientNameType.SecurityWebApi.ToString(),
                "WebServices/GetRoleByIdInTenant", new List<KeyValuePair<string, string>>
                {
                     new KeyValuePair<string, string>("roleId",roleId.ToString()),
                      new KeyValuePair<string, string>("tenantId",currentUserTenantId)
                }, string.Empty, false);


            return role;
        }
        private async Task<IActionResult> SaveDynamicRoleClaims(int roleId, List<string> actionIds, string roleClaimType)
        {
            if (roleId == 0)
                return Ok(new { success = false });

            var client = _httpClientFactory.CreateClient(HttpClientNameType.SecurityWebApi.ToString());

            List<KeyValuePair<string, string>> bodyProperties = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("roleId", roleId.ToString()),
                new KeyValuePair<string, string>("roleClaimType", roleClaimType),
                new KeyValuePair<string, string>("tenantId", _httpContextAccessor.HttpContext.User.Identity.GetUserTenantId())
            };

            foreach (var actionId in actionIds)
                bodyProperties.Add(new KeyValuePair<string, string>("actionIds[]", actionId));

            //foreach (var actionId in actionIds)
            //    bodyProperties.Add(new KeyValuePair<string, string>("SelectedRoleClaimValues[]", actionId));

            var dataContent = new FormUrlEncodedContent(bodyProperties.ToArray());
            HttpResponseMessage response = await client.PostAsync("WebServices/SaveRoleClaims", dataContent);

            return Ok(await response.Content.ReadAsStringAsync());
        }
        private async Task<List<Role>> GetAllRolesAsync(int? applicationId)
        {
            var roles = await HttpClientFactoryRequest.Get<List<Role>>
                .GetDataByClientNameAsync(_httpClientFactory, HttpClientNameType.SecurityWebApi.ToString(),
                            "WebServices/GetAllRolesAsync", null, string.Empty, false);

            roles = roles.Where(x => x.ApplicationId == applicationId).ToList();

            return roles;
        }

        [HttpGet]
        [DisplayName("مشاهده سطح دسترسی")]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> UpdateAccess([FromQuery] int? RoleId)
        {
            if (!RoleId.HasValue)

                return BadRequest();


            var role = await this.GetRoleByIdInTenant(RoleId.Value).ConfigureAwait(false);
            if (role == null)
                return BadRequest();

            ICollection<MvcControllerViewModel> securedControllerActions = null;

            securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);


            // var userRoles = CommonMethods.GetUserRoles().Select(s => s.Value);


            foreach (var item in securedControllerActions)
            {
                foreach (var item2 in item.MvcActions)
                {
                    item2.ActionIdNonSecure = _hashingService.GetSha256Hash(item2.ActionId);
                }
            }

            return Ok(new { securedControllerActions = securedControllerActions, role = role });
        }
         
        [HttpPost]
        [DisplayName("ویرایش سطح دسترسی")]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> UpdateAccessSubmit([FromQuery] int roleId, [FromBody] List<string> actionIds)
        {
            var roleClaimType = ConstantPolicies.DynamicPermission;

            if (actionIds == null)
                actionIds = new List<string>();

            return await this.SaveDynamicRoleClaims(roleId, actionIds, roleClaimType).ConfigureAwait(false);
        }

        [HttpPost]
        [DisplayName("دسترسی های داینامیک")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [IgnoreAntiforgeryToken]
        public async Task<List<string>> RawDynamicPermissions([FromBody] List<int> roleIds)
        {
            return await _accountServices.GetRawDynamicPermissions(roleIds);
        }
    }
}
