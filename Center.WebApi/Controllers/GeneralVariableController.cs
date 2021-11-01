using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.Common.Dtos.GeneralVariablesDto;
using Framework.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Center.WebApi.Controllers
{
    [DisplayName("متغیر های سیستم")]
    public class GeneralVariableController :   ApiBaseController
    {
        private readonly IGeneralVariableServices generalVariableServices;

        public GeneralVariableController(IGeneralVariableServices generalVariableServices)
        {
            this.generalVariableServices = generalVariableServices;
        }

        [HttpPost]
        [DisplayName("متیغرهای پایه با کد سیستم")]
        public async Task<ActionResult<List<SelectListDto>>> StandardVariables(List<int> systemCodeIds)
            => await generalVariableServices.StandardVariables(systemCodeIds);

        [HttpGet]
        [DisplayName("متیغرهای پایه با کد والد")]
        public async Task<ActionResult<SelectListDto>> StandardVariablesByParentId(int parentId)
            => await generalVariableServices.CitiesByStateId(parentId);
 
        [HttpGet]
        [DisplayName("متیغرهای پایه با کد زیرشاخه")]
        public async Task<ActionResult<SelectListDto>> StandardVariablesByChildId(int childId)
            => await generalVariableServices.StateByCityId(childId);
    }

}
