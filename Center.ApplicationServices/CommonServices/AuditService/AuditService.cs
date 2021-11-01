using Center.Common.Enums;
using Core.Common.Enums;
using Framework.AuditBase.Dtos;
using Framework.AuditBase.ViewModel;
using Framework.RemoteData;
using Framework.RemoteData.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.AuditService
{
    public class AuditService : IAuditService
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;

        #endregion

        #region Ctor

        public AuditService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        #endregion

        #region Methods

        public async Task<List<SlogUIViewModel>> GetEntityRawSlogsAsync(CenterDomainEntitiesType centerDomainEntitiesType, long primaryKey, CancellationToken cancellationToken = default)
        {
            var entityRawSlogsDto = new EntityRawSlogsDto
            {
                ApplicationId = Applications.Center,
                EntityName = centerDomainEntitiesType.ToString(),
                //EntityPrimaryKey = primaryKey
            };

            var model = new PostClientRequestDto<EntityRawSlogsDto>
            {
                ApiUrl = "api/v1/Audit/GetEntityRawSlogs",
                ClientFactory = _httpClientFactory,
                ClientName = nameof(HttpClientNameType.AuditingWebApi),
                InputModel = entityRawSlogsDto
            };

            var data = await APIRequest.Post<EntityRawSlogsDto, List<SlogUIViewModel>>.PostDataAsync(model);

            return data.Output;
        }

        #endregion


    }
}
