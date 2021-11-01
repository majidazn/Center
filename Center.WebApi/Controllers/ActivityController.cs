using Center.ApplicationServices.Activity.Commands.CreateActivityCommand;
using Center.ApplicationServices.Activity.Commands.CreateBulkActivityByMainApplicationCommand;
using Center.ApplicationServices.Activity.Commands.RemoveActivityCommand;
using Center.ApplicationServices.Activity.Serices;
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
    [DisplayName("برنامه ")]
    public class ActivityController : ApiBaseController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly IActivityService _activityService;

        #endregion

        #region Constructor

        public ActivityController(IMediator mediator,
            IActivityService activityService)
        {
            _mediator = mediator;
            _activityService = activityService;
        }

        #endregion

        [DisplayName("تخصیص برنامه به مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> CreateActivity(CreateActivityCommand createActivityCommand,
            CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(createActivityCommand, cancellationToken));
        }

        [HttpPost]
        [DisplayName("حذف  فعالیت مرکز")]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RemoveActivity(RemoveActivityCommand removeActivityCommand,
            CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(removeActivityCommand, cancellationToken));

        [DisplayName("تخصیص تمام برنامه های یک برنامه اصلی به مرکز")]
        [HttpPost]
        [ApiResult]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> CreateActivitiesByMainApplication(
            CreateBulkActivityByMainApplicationCommand createBulkActivityByMainApplicationCommand,
            CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(createBulkActivityByMainApplicationCommand, cancellationToken));
        }

        [HttpGet]
        [DisplayName("خواندن فعالیت های یک مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetActivitiesByCenterId(int centerId)
              => Ok(await _activityService.GetActivitiesByCenterId(centerId));

        [HttpGet]
        [DisplayName("خواندن والد فعالیت های یک مرکز")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetActivitiesParentByCenterId(int centerId)
                 => Ok(await _activityService.GetActivitiesParentByCenterId(centerId));

        [HttpGet]
        [DisplayName("خواندن فعالیت های یک مرکز با شناسه والد")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ConstantPolicies.DynamicPermission)]
        [CustomAuthorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetActivitiesByParentAndCenterId(int centerId, int parentId)
         => Ok(await _activityService.GetActivitiesByParentAndCenterId(centerId, parentId));


        [HttpGet]
        [DisplayName("خواندن فعالیت های یک تننت")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetActiveApplications(int tenantId)
            => Ok(await _activityService.GetActiveApplications(tenantId));

        [HttpGet]
        [DisplayName("خواندن فعالیت های یک تننت برای cloud")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetActiveCloudApplications(int tenantId)
            => Ok(await _activityService.GetActiveCloudApplications(tenantId));

        [HttpGet]
        [DisplayName("خواندن همه فعالیت های یک مرکز ")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCenterApplications(int tenantId)
             => Ok(await _activityService.GetCenterApplications(tenantId));

        [DisplayName("دریافت لیست برنامه ها")]
        [HttpGet]
        [ApiResult]
        public async Task<IActionResult> GetApplications()
             => Ok(await _activityService.GetApplications());
    }
}
