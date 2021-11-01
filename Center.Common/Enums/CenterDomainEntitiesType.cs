using Framework.AuditBase.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Enums
{
    public enum CenterDomainEntitiesType
    {
        [EntityInformation("Center" , "Centers", "Id")]
        Center,

        [EntityInformation("Center", "CenterCodes", "Id")]
        CenterCode,

        [EntityInformation("Center", "CenterRefers", "Id")]
        CenterRefer,

        [EntityInformation("Center", "CenterTelecoms", "Id")]
        CenterTelecom,

        [EntityInformation("Center", "ElectronicAddresses", "Id")]
        ElectronicAddresse,

        [EntityInformation("Center", "Activities", "Id")]
        Activity,

        [EntityInformation("Center", "CenterVariables", "Id")]
        CenterVariable,
    }
}
