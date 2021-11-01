using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.DomainServices
{
    public interface ICenterVariableDomainServices
    {
        bool  IsDuplicateCode(int code);
        bool IsDublicatedName(string name, int? code, int? parentId, int centervariableId, CancellationToken cancellationToken = default);
        bool IsAssignedThisApplicationToThisCenter(int centerVariableId, int centerId);
        int GetCentervariableInternalUsage(int centerVariableId);
        Task<bool> IsAssignedThisVariableToACenter(int centerVariableId, CancellationToken cancellationToken=default);
        Task<int> GenerateCenterVariableId(CancellationToken cancellationToken = default);
        bool IsAssignableApplicationToCenter(int centerVariableId, int centerId);
        bool IsCenterVariableAppliction(int centerVariableId);
        Task<bool> HasCenterVariableAnyChild(int centerVariableId, CancellationToken cancellationToken);
        bool IsCenterVariableInternalUsage(int centerVariableId);
    }
}
