using Center.Common.Dtos.GeneralVariablesDto;
using Core.Common.Enums;
using Framework.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Extensions
{
    public static class GeneralHelperExtensions
    {
        public static string ToTitleString(this int id, List<SelectListDto> selectListDtos, SystemCodes systemCodes)
        {
            try
            {
                return selectListDtos.FirstOrDefault(x => x.SystemCodeId == (int)systemCodes)
                .SelectListDtos.Where(x => x.Value == id).FirstOrDefault().Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ToParentNumber(this int id, List<SelectListDto> selectListDtos, SystemCodes systemCodes)
        {
            try
            {
                var parent = selectListDtos.FirstOrDefault(x => x.SystemCodeId == (int)systemCodes)
                .SelectListDtos.Where(x => x.Value == id).FirstOrDefault().Parent;

                return parent.ToInteger(0);
            }
            catch
            {
                return 0;
            }
        }

    }

}
