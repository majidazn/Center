using Center.Common.Enums;
using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.CenterVariableAggregate.Enums;
using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.DataAccess.Infrastrutures.Strategies
{
    public class InternalUsageRules : IInternalUsageRules
    {
        public Dictionary<InternalUsage, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>>
                    InternalUsageRule => new Dictionary<InternalUsage, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>>
                    {
                          { InternalUsage.EPD, EpdInternalUsage },
                          { InternalUsage.Centers, CenterInternalUsage },
                          { InternalUsage.Patient, PatientInternalUsage },
                          { InternalUsage.WorkHour, WorkHourInternalUsage },
                          { InternalUsage.NoData, NoDataInternalUsage }
                    };

        public Dictionary<bool, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>> InternalUsageByTenantRule
            => new Dictionary<bool, Func<IQueryable<CenterVariable>, IQueryable<CenterVariable>>> {
                  { true, EpdPayvandInternalUsage },
                  { false, NonPayvandInternalUsage }
        };

        private IQueryable<CenterVariable> EpdInternalUsage(IQueryable<CenterVariable> centerVariables)
            => centerVariables.Where(q => q.Id.Value == (int)ProjectType.HISCloud ||
                       q.Id.Value == (int)ProjectType.UtilitiesUnderCloud);

        private IQueryable<CenterVariable> CenterInternalUsage(IQueryable<CenterVariable> centerVariables)
          => centerVariables.Where(q => q.Id.Value == (int)ProjectType.HISCloud ||
                q.Id.Value == (int)ProjectType.UtilitiesUnderCloud ||
                q.Id.Value == (int)ProjectType.MIS ||
                q.Id.Value == (int)ProjectType.HISWindowsBase);

        private IQueryable<CenterVariable> WorkHourInternalUsage(IQueryable<CenterVariable> centerVariables)
         => centerVariables.Where(q => q.Id.Value == (int)ProjectType.UtilitiesUnderCloud);

        private IQueryable<CenterVariable> PatientInternalUsage(IQueryable<CenterVariable> centerVariables)
         => centerVariables.Where(q => q.Id.Value == (int)ProjectType.HISCloud ||
                 q.Id.Value == (int)ProjectType.UtilitiesUnderCloud);

        private IQueryable<CenterVariable> NoDataInternalUsage(IQueryable<CenterVariable> centerVariables)
         => centerVariables;

        private IQueryable<CenterVariable> EpdPayvandInternalUsage(IQueryable<CenterVariable> centerVariables)
          => centerVariables.Where(q => q.InternalUsage == (int)InternalUsage.EPD || q.InternalUsage == (int)InternalUsage.CentersEPD);

        private IQueryable<CenterVariable> NonPayvandInternalUsage(IQueryable<CenterVariable> centerVariables)
            => centerVariables.Where(q => q.InternalUsage == (int)InternalUsage.Centers || q.InternalUsage == (int)InternalUsage.CentersEPD);
    }
}
