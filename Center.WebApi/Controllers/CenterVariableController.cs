using Center.ApplicationServices.CenterVariable.Commands.CreateCenterVariableCommand;
using Center.ApplicationServices.CenterVariable.Commands.EditCenterVariableCommand;
using Center.ApplicationServices.CenterVariable.Commands.RemoveCenterVariableCommand;
using Center.ApplicationServices.CenterVariable.Commands.SortCenterVariableCommand;
using Center.ApplicationServices.CenterVariable.Queries;
using Center.ApplicationServices.CenterVariable.Services;
using Center.Common.Dtos;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Framework.Api;
using Framework.Attributes;
using Framework.Controller;
using Framework.DynamicPermissions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{

    [DisplayName("متغیرها ")]
    public class CenterVariableController : ApiBaseController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ICenterVariableService _centerVariableService;

        #endregion

        #region Constructor

        public CenterVariableController(IMediator mediator,
            ICenterVariableService centerVariableService)
        {
            _mediator = mediator;
            _centerVariableService = centerVariableService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [DisplayName("برنامه های فعال مرکز")]
        public async Task<IActionResult> GetActiveApplications(int tenantId)
        => Ok(await _centerVariableService.GetActiveApplications(tenantId));

        [HttpGet]
        [DisplayName("برنامه های فعال مرکز و InternalUsage")]
        public async Task<IActionResult> GetActiveApplicationsByInernalUsage(int tenantId, int appType = 0)
        => Ok(await _centerVariableService.GetActiveApplicationsByInernalUsage(tenantId, appType));

        [DisplayName("افزودن متغییر های مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> CreateCenterVariable(CreateCenterVariableCommand createCenterVariableCommand, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(createCenterVariableCommand, cancellationToken));

        [DisplayName("ویرایش متغییر های مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> EditCenterVariable(EditCenterVariableCommand editCenterVariableCommand, CancellationToken cancellationToken = default)
           => Ok(await _mediator.Send(editCenterVariableCommand, cancellationToken));

        [HttpPost]
        [DisplayName("حذف  متغییر های مرکز")]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RemoveCenterVariable(RemoveCenterVariableCommand removeCenterVariableCommand, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(removeCenterVariableCommand, cancellationToken));

        [HttpPost]
        [DisplayName("جستجو")]
        //[ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult<GridResultDto<CenterVariableResultSearchDto>>> SearchCenterVariable(SearchCenterVariableDto dto)
          => Ok(await _mediator.Send(new CenterVariableSearchQuery() { SearchDto = dto }));

        [HttpGet]
        [DisplayName("خواندن متغیر ها با parentId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetCenterVariablesByParentId(int parentId)
           => Ok(await _centerVariableService.GetCenterVariablesByParentId(parentId));


        [HttpGet]
        [DisplayName("خواندن متغیر ها با parentId و Internal usage")]
        public async Task<IActionResult> GetCenterVariablesByParentIdAndInternalUsage(int parentId, int internalUsage)
         => Ok(await _centerVariableService.GetCenterVariablesByParentIdAndInternalUsage(internalUsage, parentId));

        [HttpGet]
        [DisplayName("خواندن متغیر ها")]
        public async Task<IActionResult> GetCenterVariables(int internalUsage)
          => Ok(await _centerVariableService.GetCenterVariables(internalUsage));

        [DisplayName("مرتب سازی متغیر های مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> SortCenterVariables(SortCenterVariableCommand sortCenterVariableCommand, CancellationToken cancellationToken = default)
        => Ok(await _mediator.Send(sortCenterVariableCommand, cancellationToken));

        [HttpGet]
        [DisplayName("برنامه های فعال مرکز ")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetCenterVariablesWithActiveApplications(int parentId, int tenantId, int centerId)
             => Ok(await _centerVariableService.GetCenterVariablesWithActiveApplications(parentId, centerId, tenantId));
 
        [HttpGet]
        [DisplayName("خواندن متغیر ها با و خود پرنت parentId")]
        public async Task<IActionResult> GetCenterVariablesWithParentByParentId(int parentId)
        => Ok(await _centerVariableService.GetCenterVariablesWithParentByParentId(parentId));

        [HttpGet]
        [DisplayName("خواندن متغیر ها با فرزندان parentId")]
        public async Task<IActionResult> GetCenterVariablesWithChildrenByParentId(int parentId)
        => Ok(await _centerVariableService.GetCenterVariablesWithChildrenByParentId(parentId));

        #endregion
    }
}
