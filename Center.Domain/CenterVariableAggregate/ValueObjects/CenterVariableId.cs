using Center.Domain.CenterVariableAggregate.DomainServices;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.ValueObjects
{
    public class CenterVariableId : ValueObject, IAuditableReference
    {
        #region Properties
        public int Value { get; private set; }
        #endregion

        #region Constructors
        private CenterVariableId(int value)
        {
            this.Value = value;
        }
        #endregion

        #region Behaviors
        public static implicit operator int(CenterVariableId centerVariableId) => centerVariableId.Value;
        public static CenterVariableId Create(int centerVariableId)
        {
            return new CenterVariableId(centerVariableId);
        }

        public static CenterVariableId Create(ICenterVariableDomainServices centerVariableDomainServices)
        {
            var centerVariableId = centerVariableDomainServices.GenerateCenterVariableId().Result;

            if (centerVariableId == 0)
                centerVariableId = 100;

            centerVariableId++;

            return new CenterVariableId(centerVariableId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion

    }
}
