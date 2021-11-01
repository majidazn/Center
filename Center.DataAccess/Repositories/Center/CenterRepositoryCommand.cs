using Center.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Repositories;
using Center.Domain.CenterAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Newtonsoft.Json;
using Framework.AuditBase.ViewModel;
using Center.Common.Enums;
using Framework.AuditBase.Extentions;

namespace Center.DataAccess.Repositories.Center
{
    public class CenterRepositoryCommand : Repository<Domain.CenterAggregate.Entities.Center>, ICenterRepositoryCommand
    {
        private readonly CenterBoundedContextCommand _centerBoundedContextCommand;
        public CenterRepositoryCommand(CenterBoundedContextCommand dbContext, IHttpContextAccessor httpContextAccessor = null)
            : base(dbContext, httpContextAccessor)
        {
            _centerBoundedContextCommand = dbContext;
        }

        public async Task<int> GenerateTenantId(CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Centers
                .IgnoreQueryFilters()
                .Where(x => x.TenantId != (byte)Tenants.EpdPayvand)
                .Select(x => x.TenantId).MaxAsync() ?? 0;


            //var tenantId = 0;
            //if (await _centerBoundedContextCommand.Centers
            //    .IgnoreQueryFilters()
            //    .Where(x => x.TenantId != (byte)Tenants.EpdPayvand)
            //    .CountAsync(cancellationToken) == 0)
            //{
            //    tenantId = 10000;
            //}
            //else
            //{
            //    tenantId = await _centerBoundedContextCommand.Centers.IgnoreQueryFilters()
            //                                 .Where(x => x.TenantId != (byte)Tenants.EpdPayvand)
            //                                 .MaxAsync(m => m.TenantId, cancellationToken);
            //    if (tenantId == 0)
            //        tenantId = 10000;
            //}

            //return ++tenantId;
        }
        public async Task<Domain.CenterAggregate.Entities.Center> FetchCenterAggregate(long centerId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Centers.
                Include(n => n.CenterTelecoms).
                Include(n => n.ElectronicAddresses).
                FirstOrDefaultAsync(e => e.Id == centerId, cancellationToken);
        }

        public async Task<Domain.CenterAggregate.Entities.Center> GetCenterById(int centerId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Centers.FirstOrDefaultAsync(q => q.Id == centerId, cancellationToken);
        }


        public async Task<bool> HasCenterActivity(int centerId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Activities.AnyAsync(a => a.CenterId == centerId);
        }

        public async Task<bool> HasCenterVariable(int centerVariableId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Centers.AnyAsync(a => a.Title == centerVariableId || a.CenterGroup == centerVariableId);
        }

    }
}
