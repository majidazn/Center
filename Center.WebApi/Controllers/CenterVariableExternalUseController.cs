using Center.ApplicationServices.CenterVariable.Services;
using Framework.Controller;
using Framework.DynamicPermissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{
    public class CenterVariableExternalUseController : ApiBaseController
    {
        private readonly ICenterVariableService _centerVariableService;
        public CenterVariableExternalUseController(ICenterVariableService centerVariableService)
        {
            _centerVariableService = centerVariableService;
        }

        [HttpGet]
        [DisplayName("برنامه های فعال هر مرکز ")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetActiveApplications(int parentId, int tenantId)
            => Ok(await _centerVariableService.GetActiveApplications(tenantId, parentId));
    }
}
