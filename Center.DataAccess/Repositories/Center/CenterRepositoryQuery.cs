using Center.Common.Dtos.GeneralVariablesDto;
using Center.DataAccess.Context;
using Center.Domain.CenterAggregate.Dtos.Center;
using Center.Domain.CenterAggregate.Repositories;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Core.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.DataAccess.Repositories.Center
{
    public class CenterRepositoryQuery : ICenterRepositoryQuery
    {
        #region Fields
        private readonly CenterBoundedContextQuery _context;
        #endregion

        #region Constructors
        public CenterRepositoryQuery(CenterBoundedContextQuery centerBoundedContextQuery)
        {
            _context = centerBoundedContextQuery;
        }
        #endregion

        #region Methods

        public IQueryable<Domain.CenterAggregate.Entities.Center> GetCenters()
        {
            var centers = from x in _context.Centers
                              // join y in _context.Employees.IgnoreQueryFilters() on x.Id equals y.PersonId
                          select x;

            return centers;
        }

        public Task<CenterFullDto> GetCenterById(int centerId)
        {
            var query = (from c in _context.Centers
                         where c.Id == centerId
                         select new CenterFullDto
                         {
                             Name = c.Name,
                             EnName = c.EnName,
                             Address = c.Address,
                             CenterGroup = c.CenterGroup,
                             CenterId = c.Id,
                             City = c.City,
                             FinanchialCode = c.FinanchialCode,
                             HostName = c.HostName,
                             Logo = c.Logo,
                             NationalCode = c.NationalCode,
                             SepasCode = c.SepasCode,
                             Title = c.Title,
                             ValidFrom = c.ValidFrom,
                             ValidTo = c.ValidTo,
                             ZipCode = c.ZipCode,
                             TenantId = c.TenantId,
                             ElectronicAddresses = c.ElectronicAddresses
                             .Select(s => new Domain.CenterAggregate.Dtos.ElectronicAddress.CreateElectronicAddressDto
                             {
                                 Id = s.Id,
                                 CenterId = s.CenterId,
                                 EAddress = s.EAddress,
                                 EType = s.EType
                             }),
                             Telecoms = c.CenterTelecoms
                             .Select(s => new Domain.CenterAggregate.Dtos.CenterTelecom.CreateTelecomDto
                             {
                                 CenterId = s.CenterId,
                                 Comment = s.Comment,
                                 CreateTelecomId = s.Id,
                                 Section = s.Section,
                                 TellNo = s.TellNo,
                                 Type = s.Type
                             })

                         }).FirstOrDefaultAsync();
            return query;
        }
        public Task<List<int>> GetTenantsByCenterIds(List<int> centerIds)
        {
            var query = (from c in _context.Centers
                         where centerIds.Contains(c.Id)
                         select c.TenantId.Value).ToListAsync();

            return query;
        }
        public Task<List<CenterInLoginPageDto>> GetCentersWithTitleAndCity(int centerGroupId)
        {
            var query = (from c in _context.Centers.IgnoreQueryFilters()
                                       .Where(x => x.Status != EntityStateType.Deleted)
                         join p in _context.CenterVariables.IgnoreQueryFilters()
                                  .Where(x => x.Status != EntityStateType.Deleted)
                              on c.CenterGroup equals p.Id
                         join s in _context.CenterVariables.IgnoreQueryFilters()
                                  .Where(x => x.Status != EntityStateType.Deleted)
                              on c.Title equals s.Id

                         where c.CenterGroup == centerGroupId
                         select new CenterInLoginPageDto
                         {
                             Name = c.Name,
                             EnName = c.EnName,
                             TenantId = c.TenantId,
                             CenterId = c.Id,
                             City = c.City.ToString(),
                             HostName = c.HostName,
                             Title = s.Name,
                             CenterGroup = p.Name
                         }).ToListAsync();
            return query;
        }
        public Task<CenterInLoginPageDto> GetCenterByHostName(string hostname)
        {
            var query = (from c in _context.Centers.IgnoreQueryFilters()
                                         .Where(x => x.Status != EntityStateType.Deleted)
                         join p in _context.CenterVariables.IgnoreQueryFilters()
                              on c.CenterGroup equals p.Id
                         join s in _context.CenterVariables.IgnoreQueryFilters()
                              on c.Title equals s.Id
                         where c.HostName == hostname
                         select new CenterInLoginPageDto
                         {
                             Name = c.Name,
                             EnName = c.EnName,
                             TenantId = c.TenantId,
                             CenterId = c.Id,
                             City = c.City.ToString(),
                             HostName = c.HostName,
                             Logo = c.Logo,
                             Title = s.Name,
                             CenterGroup = p.Name
                         }).FirstOrDefaultAsync();

            return query;
        }

        public async Task<byte[]> GetCenterLogoById(int centerId)
        {
            return (await _context.Centers.FirstOrDefaultAsync(q => q.Id == centerId)).Logo;
        }

        public async Task<byte[]> GetCenterLogoByTenantId(int tenantId)
        {
            var center = await (from x in _context.Centers.IgnoreQueryFilters()
                                          .Where(x => x.Status != EntityStateType.Deleted)
                                where x.TenantId == tenantId
                                select x).FirstOrDefaultAsync();

            return center.Logo ?? new byte[0];
        }

        public Task<List<int>> GetTenantIdsByCenterGroup(int centerGroupId)
        {
            return _context.Centers.Where(q => q.CenterGroup == centerGroupId).Select(s => s.TenantId.Value).ToListAsync();
        }

        public Task<List<CenterFullDto>> GetCenterList()
        {
            var list = (from c in _context.Centers
                        select new CenterFullDto
                        {
                            Name = c.Name,
                            EnName = c.EnName,
                            CenterId = c.Id,
                            TenantId = c.TenantId,
                            CenterGroup = c.CenterGroup,
                            Title = c.Title,
                            City = c.City
                        }).ToListAsync();
            return list;
        }
        public async Task<List<CenterFullDto>> GetCenters(int centerGroupId, List<long> cities)
        {
            var query = from c in _context.Centers
                        where cities.Contains(c.City) && (centerGroupId == 0 || c.CenterGroup == centerGroupId)
                        select new CenterFullDto
                        {
                            Name = c.Name,
                            EnName = c.EnName,
                            TenantId = c.TenantId,
                            Address = c.Address,
                            CenterGroup = c.CenterGroup,
                            CenterId = c.Id,
                            City = c.City,
                            FinanchialCode = c.FinanchialCode,
                            HostName = c.HostName,
                            Logo = c.Logo,
                            NationalCode = c.NationalCode,
                            SepasCode = c.SepasCode,
                            Title = c.Title,
                            ValidFrom = c.ValidFrom,
                            ValidTo = c.ValidTo,
                            ZipCode = c.ZipCode
                        };

            return await query.ToListAsync();
        }
        public Task<List<SelectListItemDto>> GetCentersName()
        {
            var query = from c in _context.Centers
                        select new SelectListItemDto
                        {
                            Text = c.Name,
                            Value = c.Id,
                        };
            return query.ToListAsync();
        }

        public Task<List<CenterFullDto>> GetAllCenters()
        {
            var query = from c in _context.Centers
                        select new CenterFullDto
                        {
                            Name = c.Name,
                            EnName = c.EnName,
                            TenantId = c.TenantId,
                            Address = c.Address,
                            CenterGroup = c.CenterGroup,
                            CenterId = c.Id,
                            City = c.City,
                            FinanchialCode = c.FinanchialCode,
                            HostName = c.HostName,
                            Logo = c.Logo,
                            NationalCode = c.NationalCode,
                            SepasCode = c.SepasCode,
                            Title = c.Title,
                            ValidFrom = c.ValidFrom,
                            ValidTo = c.ValidTo,
                            ZipCode = c.ZipCode
                        };

            return query.ToListAsync();
        }
        public Task<SelectListItemDto> GetCenterGroupByCenterId(int centerId)
        {
            var query = from c in _context.Centers
                        join cv in _context.CenterVariables on c.CenterGroup equals cv.Id
                        where c.Id == centerId
                        select new SelectListItemDto
                        {
                            Text = cv.Name,
                            Value = c.CenterGroup,
                        };

            return query.FirstOrDefaultAsync();
        }
        public Task<List<CenterGroupDto>> GetCenterGroupByCenterIds(List<int> centerIds)
        {
            var query = from c in _context.Centers
                        join cv in _context.CenterVariables on c.CenterGroup equals cv.Id
                        where centerIds.Contains(c.Id)
                        select new CenterGroupDto
                        {
                            CenterId = c.Id,
                            CenterGroupId = c.CenterGroup,
                            CenterGroupName = cv.Name
                        };

            return query.ToListAsync();
        }
    }
    #endregion
}
