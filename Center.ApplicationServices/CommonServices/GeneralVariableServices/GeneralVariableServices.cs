using Center.Common.Api;
using Center.Common.Dtos.GeneralVariablesDto;
using Core.Common.Enums;
using Framework.Api;
using Framework.Caching.Services;
using Framework.RemoteData;
using Framework.RemoteData.Dtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.GeneralVariableServices
{
    public class GeneralVariableServices : IGeneralVariableServices
    {
        public static List<SelectListDto> GeneralVariableData;
        private readonly IMemoryCachingServices memoryCaching;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptionsSnapshot<SiteSettings> siteSettings;
        private readonly string CacheKey = "GeneralVariables";

        public GeneralVariableServices(IMemoryCachingServices memoryCaching
            , IHttpClientFactory httpClientFactory
            , IOptionsSnapshot<SiteSettings> siteSettings)
        {
            this.memoryCaching = memoryCaching;
            this.httpClientFactory = httpClientFactory;
            this.siteSettings = siteSettings;
        }

        public Task<List<SelectListDto>> FillStandardVariables()
        {
            var data = Task.FromResult(new List<SelectListDto>());
            if (memoryCaching.IsInCache(CacheKey))
                data = CachedStandardVariables(data);
            else
            {
                data = GetStandardVariables();
                _ = CachedStandardVariables(data);
            }

            return data;
        }
        public async Task<SelectListDto> States()
        {
            var states = (await FillStandardVariables())
                        .Where(x => x.SystemCodeId == (int)SystemCodes.State).FirstOrDefault();
            return states;
        }
        public Task<SelectListDto> CitiesByStateId(int stateId)
                => GetStandardVariablesByParentId(stateId);
        public Task<SelectListDto> StateByCityId(int cityId)
            => GetStandardVariablesByChildId(cityId, (int)SystemCodes.City);
        public async Task<List<SelectListDto>> StandardVariables(List<int> systemCodeIds)
        {
            var data = await FillStandardVariables();
            return data.Where(x => systemCodeIds.Contains(x.SystemCodeId)).ToList();
        }

        private Task<List<SelectListDto>> CachedStandardVariables(Task<List<SelectListDto>> data)
            => memoryCaching.GetOrCreateAsync<List<SelectListDto>>(CacheKey,
                           () => (data), TimeSpan.FromMinutes(siteSettings.Value.DataCacheTimeInMinute));

        private async Task<SelectListDto> GetStandardVariablesByParentId(int parentId)
        {
            var data = await FillStandardVariables();
            var selectListDto = new SelectListDto();
            selectListDto.SelectListDtos = new List<SelectListItemDto>();
            foreach (var item in data)
            {
                var selectListItem = item.SelectListDtos.Where(x => x.Parent == parentId);

                if (selectListItem.Count() > 0)
                {
                    selectListDto.SelectListDtos =
                          (from selectList in selectListItem
                           select new SelectListItemDto
                           {
                               Parent = selectList.Parent,
                               Selected = true,
                               Text = selectList.Text,
                               Value = selectList.Value
                           }).ToList();

                    selectListDto.SystemCodeId = item.SystemCodeId;
                    selectListDto.ParentId = item.ParentId;
                    selectListDto.SystemCodeName = item.SystemCodeName;
                }
            }

            return selectListDto;
        }
        private async Task<SelectListDto> GetStandardVariablesById(int id)
        {
            var data = await FillStandardVariables();
            var selectListDto = new SelectListDto();
            selectListDto.SelectListDtos = new List<SelectListItemDto>();

            foreach (var item in data)
            {
                var selectListItem = item.SelectListDtos.Where(x => x.Value == id);

                if (selectListItem.Count() > 0)
                {
                    selectListDto.SelectListDtos =
                        (from selectList in selectListItem
                         select new SelectListItemDto
                         {
                             Parent = selectList.Parent,
                             Selected = true,
                             Text = selectList.Text,
                             Value = selectList.Value
                         }).ToList();

                    selectListDto.SystemCodeId = item.SystemCodeId;
                    selectListDto.ParentId = item.ParentId;
                    selectListDto.SystemCodeName = item.SystemCodeName;
                }
            }

            return selectListDto;
        }
        private async Task<SelectListDto> GetStandardVariablesByChildId(int childId, int systemCodeId)
        {
            var selectListDto = new SelectListDto();
            var data = await FillStandardVariables();
            selectListDto.SelectListDtos = new List<SelectListItemDto>();

            var selectList = data.Where(x => x.SystemCodeId == systemCodeId).FirstOrDefault();
            var parent = selectList.SelectListDtos.Where(x => x.Value == childId).FirstOrDefault().Parent;

            return await GetStandardVariablesById(Convert.ToInt32(parent));
        }
        private async Task<List<SelectListDto>> GetStandardVariables()
        {
            var lstSystemCodeIds = new List<int>();
            lstSystemCodeIds.Add((int)SystemCodes.City);
            lstSystemCodeIds.Add((int)SystemCodes.State);
            lstSystemCodeIds.Add((int)SystemCodes.Country);

            var model = new PostClientRequestDto<List<int>>
            {
                ApiUrl = "/api/StandardVariable/GetStandardVariablesBySystemCodeIds",
                ClientFactory = httpClientFactory,
                ClientName = nameof(HttpClientNameType.VariableWebApi),
                InputModel = lstSystemCodeIds
            };

            var dataModel = await APIRequest.Post<List<int>, ApiResult<List<StandardVariablesDto>>>.PostDataAsync(model);

            var lstSelectListDto = new List<SelectListDto>();
            if (dataModel.IsSucceeded && dataModel.Output != null)
            {
                var isParent = false;
                var data = dataModel.Output.Data;
                var lstSelectListItemDto = new List<SelectListItemDto>();

                data = data.OrderBy(x => x.SystemCodeId).ThenBy(x => x.Name).ToList();

                foreach (var systemCode in lstSystemCodeIds)
                {
                    isParent = false;
                    lstSelectListItemDto = new List<SelectListItemDto>();

                    foreach (var item in data.Where(x => x.SystemCodeId == (int)systemCode))
                    {
                        isParent = true;
                        lstSelectListItemDto.Add(new SelectListItemDto
                        {
                            Selected = false,
                            Text = item.Name,
                            Parent = item.Parent,
                            Value = item.StandardVariableId
                        });
                    }

                    if (isParent)
                        lstSelectListDto.Add(new SelectListDto
                        {
                            SystemCodeId = (int)systemCode,
                            SelectListDtos = lstSelectListItemDto,
                            SystemCodeName = ((SystemCodes)systemCode).ToString(),
                            ParentId = data.Where(x => x.SystemCodeId == (int)systemCode)?.FirstOrDefault()?.Parent ?? 0,
                            VariableId = data.Where(x => x.SystemCodeId == (int)systemCode)?.FirstOrDefault()?.StandardVariableId ?? 0
                        });
                }
            }

            return lstSelectListDto;
        }
    }
}