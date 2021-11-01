using Center.Common.Enums;
using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.CenterVariableAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.DataAccess.Infrastrutures.Strategies
{
    public interface IInternalUsageRules
    {
        Dictionary<InternalUsage, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>> InternalUsageRule { get; }
        Dictionary<bool, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>> InternalUsageByTenantRule { get; }

    }
}
