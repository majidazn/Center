using Center.Domain.CenterAggregate.Entities;
using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Center.DataAccess.Context
{
    public class CenterBoundedContextQuery : IDisposable
    {
        #region Fields

        private readonly CenterBoundedContextCommand _context;
        private bool _disposed = false;

        #endregion

        #region Properties

        public CenterBoundedContextQuery(CenterBoundedContextCommand context)
        {
            this._context = context;
        }
        internal IQueryable<Domain.CenterAggregate.Entities.Center> Centers
        {
            get { return _context.Centers; }
        }
        internal IQueryable<CenterVariable> CenterVariables
        {
            get { return _context.CenterVariables; }
        }
        internal IQueryable<Activity> Activities
        {
            get { return _context.Activities; }
        }
        internal IQueryable<CenterCode> CenterCodes
        {
            get { return _context.CenterCodes; }
        }
        internal IQueryable<CenterRefer> CenterRefers
        {
            get { return _context.CenterRefers; }
        }
        internal IQueryable<CenterTelecom> CenterTelecoms
        {
            get { return _context.CenterTelecoms; }
        }
        internal IQueryable<ElectronicAddress> ElectronicAddresses
        {
            get { return _context.ElectronicAddresses; }
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
