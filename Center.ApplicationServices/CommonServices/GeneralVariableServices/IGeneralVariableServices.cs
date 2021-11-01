using Center.Common.Dtos.GeneralVariablesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.GeneralVariableServices
{
    public interface IGeneralVariableServices
    {
        Task<SelectListDto> CitiesByStateId(int stateId);
        Task<SelectListDto> States();
        Task<SelectListDto> StateByCityId(int cityId);
        Task<List<SelectListDto>> StandardVariables(List<int> systemCodeIds);
        Task<List<SelectListDto>> FillStandardVariables();
    }
}
