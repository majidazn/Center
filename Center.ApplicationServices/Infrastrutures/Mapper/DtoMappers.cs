using Center.Common.Dtos.GeneralVariablesDto;
using Center.Common.Extensions;
using Center.Domain.CenterAggregate.Dtos.Center;
using Core.Common.Enums;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Infrastrutures.Mapper
{
    public static class DtoMappers
    {
        public static CenterFullDto MapCenterDto(List<SelectListDto> general, CenterFullDto Center)
        {
            Center.ValidFromPersian = Center.ValidFrom.ToShamsiDateWithPersianNumber();
            Center.ValidtoPersian = Center.ValidTo != null ? Center.ValidTo.Value.ToShamsiDateWithPersianNumber() : "";
            // Center.TitleDisplayString = Center.TitleDisplay.ToTitleString(general, SystemCodes.Title);

            return Center;
        }

        public static async Task<(string, string)> MapCenterInSearchResultDto(List<SelectListDto> general, int cityId)
        {
            var stateId = cityId.ToParentNumber(general, SystemCodes.City);

            var city = cityId.ToTitleString(general, SystemCodes.City);
            var state = stateId.ToTitleString(general, SystemCodes.State);

            return (city, state);
        }

        public static CenterInLoginPageDto MapCenterInLogginPageDto(List<SelectListDto> general, CenterInLoginPageDto center)
        {
            var cityId = center.City.ToInteger(0);
            var stateId = cityId.ToParentNumber(general, SystemCodes.City);
            var countryId = stateId.ToParentNumber(general, SystemCodes.State);

            center.City = cityId.ToTitleString(general, SystemCodes.City);
            center.State = stateId.ToTitleString(general, SystemCodes.State);
            center.Country = countryId.ToTitleString(general, SystemCodes.Country);

            return center;
        }

        public static List<CenterFullDto> MapCenterDtos(List<SelectListDto> general, List<CenterFullDto> centers)
        {
            var lstCenters = new List<CenterFullDto>();
            foreach (var Center in centers)
                lstCenters.Add(MapCenterDto(general, Center));

            return lstCenters;
        }
        public static List<CenterInLoginPageDto> MapCentersWithTitleAndCity(List<SelectListDto> general, List<CenterInLoginPageDto> centers)
        {
            var lstCenters = new List<CenterInLoginPageDto>();
            foreach (var center in centers)
                lstCenters.Add(MapCenterInLogginPageDto(general, center));

            return lstCenters;
        }
    }
}
