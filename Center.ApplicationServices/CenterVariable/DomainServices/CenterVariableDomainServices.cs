using Center.Domain.CenterVariableAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.CenterVariableAggregate.Repositories;
using Core.Common.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.DomainServices
{
    public class CenterVariableDomainServices : ICenterVariableDomainServices
    {
        private readonly ICenterVariableRepositoryCommand _centerVariableRepositoryCommand;

        public CenterVariableDomainServices(ICenterVariableRepositoryCommand centerVariableRepositoryCommand) //query
        {
            _centerVariableRepositoryCommand = centerVariableRepositoryCommand;
        }

        public bool IsDublicatedName(string name, int? code, int? parentId, int centervariableId, CancellationToken cancellationToken) =>
                  _centerVariableRepositoryCommand.IsDublicatedName(name, code, parentId, centervariableId, cancellationToken);

        public bool IsAssignedThisApplicationToThisCenter(int centerVariableId, int centerId) =>
            _centerVariableRepositoryCommand.IsAssignedThisApplicationToThisCenter(centerVariableId, centerId);

        public int GetCentervariableInternalUsage(int centerVariableId) =>
            _centerVariableRepositoryCommand.FirstOrDefault(q => q.Id == centerVariableId).InternalUsage;

        public async Task<bool> IsAssignedThisVariableToACenter(int centerVariableId, CancellationToken cancellationToken) =>
              await _centerVariableRepositoryCommand.IsAssignedThisVariableToACenter(centerVariableId, cancellationToken);

        public async Task<int> GenerateCenterVariableId(CancellationToken cancellationToken = default) =>
            await _centerVariableRepositoryCommand.GenerateCenterVariableId(cancellationToken);

        public bool IsAssignableApplicationToCenter(int centerVariableId, int centerId) =>
          _centerVariableRepositoryCommand.IsAssignableApplicationToCenter(centerVariableId, centerId);

        public bool IsCenterVariableAppliction(int centerVariableId)
        {
            var parentId = _centerVariableRepositoryCommand.FirstOrDefault(q => q.Id == centerVariableId).ParentId;

            if (parentId == (int)CenterVariableType.Applications)
                return true;
            return false;
        }

        public bool IsCenterVariableInternalUsage(int centerVariableId)
        {
            var parentId = _centerVariableRepositoryCommand.FirstOrDefault(q => q.Id == centerVariableId).ParentId;

            if (parentId == (int)CenterVariableType.InternalUsage)
                return true;
            return false;
        }

        public async Task<bool> HasCenterVariableAnyChild(int centerVariableId, CancellationToken cancellationToken) =>
                await _centerVariableRepositoryCommand.HasCenterVariableAnyChild(centerVariableId, cancellationToken);

        public bool IsDuplicateCode(int code)
        {
            if (code == default)
                return false;

            return _centerVariableRepositoryCommand.IsDublicatedCode(code);
        }
    }
}
