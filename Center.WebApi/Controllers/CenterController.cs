using Center.ApplicationServices.Center.Commands.CreateCenterCommand;
using Center.ApplicationServices.Center.Commands.EditCenterCommand;
using Center.ApplicationServices.Center.Commands.RemoveCenterCommand;
using Center.ApplicationServices.Center.Queries;
using Center.ApplicationServices.Center.Services;
using Center.Common.Dtos;
using Center.Domain.CenterAggregate.Dtos.Center;
using Framework.Api;
using Framework.Attributes;
using Framework.Controller;
using Framework.DynamicPermissions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{
    [DisplayName("مرکز")]
    public class CenterController : ApiBaseController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ICenterService _centerService;

        #endregion

        #region Constructor

        public CenterController(IMediator mediator,
            ICenterService centerService)
        {
            _mediator = mediator;
            _centerService = centerService;
        }

        #endregion

        #region Methods

        [DisplayName("افزودن مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> CreateCenter(CreateCenterCommand createCenterCommand, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(createCenterCommand, cancellationToken));

        [DisplayName("ویرایش مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> EditCenter(EditCenterCommand editCenterCommand, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(editCenterCommand, cancellationToken));

        [HttpPost]
        [DisplayName("جستجو")]
        //[ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult<GridResultDto<CenterResultSearchDto>>> SearchCenter(SearchDto dto)
        => Ok(await _mediator.Send(new CenterSearchQuery() { SearchDto = dto }));

        [HttpPost]
        [DisplayName("حذف مرکز")]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RemoveCenter(RemoveCenterCommand removeCenterCommand, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(removeCenterCommand, cancellationToken));

        [HttpGet]
        [DisplayName("خواندن مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterById(int centerId)
            => Ok(await _centerService.GetCenterById(centerId));

        [HttpPost]
        [DisplayName("دریافت تننت مراکز")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTenantsByCenterIds(List<int> centerIds)
                => Ok(await _centerService.GetTenantsByCenterIds(centerIds));

        [HttpGet]
        [DisplayName("خواندن لوگو مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterLogoById(int centerId)
              => Ok(await _centerService.GetCenterLogoById(centerId));

        [HttpGet]
        [DisplayName(" خواندن لوگو مرکز با TenantId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterLogoByTenantId(int tenantId)
            => Ok(await _centerService.GetCenterLogoByTenantId(tenantId));

        [HttpGet]
        [DisplayName(" خواندن مرکز با آدرس مرکز")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCenterByHostName(string hostname)
              => Ok(await _centerService.GetCenterByHostName(hostname));

        [HttpGet]
        [DisplayName("لیست TenantId با گروه مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTenantIdsByCenterGroup(int centerGroupId)
             => Ok(await _centerService.GetTenantIdsByCenterGroup(centerGroupId));

        [HttpGet]
        [DisplayName("لیست تمامی مراکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterList()
            => Ok(await _centerService.GetCenterList());

        [HttpGet]
        [DisplayName("لیست تمامی مراکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCentersByFilters(int stateId, int centerGroupId)
              => Ok(await _centerService.GetCenters(stateId, centerGroupId));

        [HttpGet]
        [DisplayName("لیست تمامی مراکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCentersName()
            => Ok(await _centerService.GetCentersName());

        [HttpGet]
        [DisplayName("لیست تمامی مراکز به همراه نام استان و شهر")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllCenters()
         => Ok(await _centerService.GetAllCenters());

        [HttpGet]
        [DisplayName("گروه مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterGroupByCenterId(int centerId)
       => Ok(await _centerService.GetCenterGroupByCenterId(centerId));

        [HttpPost]
        [DisplayName("گروه مراکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterGroupByCenterIds([FromBody] List<int> centerIds)
                => Ok(await _centerService.GetCenterGroupByCenterIds(centerIds));

        [HttpGet]
        [DisplayName("لیست مراکز به همراه عنوان و نام شهر")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCentersWithTitleAndCity(int centerGroupId)
               => Ok(await _centerService.GetCentersWithTitleAndCity(centerGroupId));

        #endregion
    }
}
