using Center.ApplicationServices.CommonServices.AuditService;
using Center.Common.Enums;
using Center.Domain.CenterAggregate.Repositories;
using Framework.Api;
using Framework.Attributes;
using Framework.Controller;
using Framework.DynamicPermissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{
    [ApiResult]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("سوابق تغییرات")]
    [Route("api/[controller]")]
    public class SlogController : ApiBaseController
    {
        private readonly IAuditService _auditService;

        public SlogController(IAuditService auditService)
        {
            this._auditService = auditService;

        }

        [DisplayName("دریافت سوابق تغییرات یک رکورد جدول")]
        [HttpGet]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetEntityRawSlogs(CenterDomainEntitiesType centerDomainEntitiesType, long primaryKey, CancellationToken cancellationToken = default)
             => Ok(await _auditService.GetEntityRawSlogsAsync(centerDomainEntitiesType, primaryKey, cancellationToken));

    }
}
