using Center.Common.Api;
using Center.Common.Enums;
using Center.Common.Extensions;
using Center.DataAccess.Context;
using Center.DataAccess.Infrastrutures.Strategies;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.CenterVariableAggregate.Repositories;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Center.Domain.SharedKernel.Dtos.Activity;
using Core.Common.Enums;
using Framework.Caching.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.DataAccess.Repositories.CenterVariable
{
    public class CenterVariableRepositoryQuery : ICenterVariableRepositoryQuery
    {
        #region Fields
        private readonly CenterBoundedContextQuery _context;
        private readonly IMemoryCachingServices _memoryCaching;
        private readonly string CacheKey = "CenterVariables";
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;
        private readonly IInternalUsageRules _internalUsageRules;
        #endregion

        #region Constructors
        public CenterVariableRepositoryQuery(CenterBoundedContextQuery centerBoundedContextQuery,
            IMemoryCachingServices memoryCaching,
             IOptionsSnapshot<SiteSettings> siteSettings,
            IInternalUsageRules internalUsageRules)
        {
            _context = centerBoundedContextQuery;
            _memoryCaching = memoryCaching;
            _siteSettings = siteSettings;
            _internalUsageRules = internalUsageRules;
        }
        #endregion

        #region Methods

        public IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> GetCenterVariables()
        {
            var centerVariables = from x in _context.CenterVariables
                                  orderby x.Sort
                                  select x;

            return centerVariables;
        }

        public async Task<List<CenterVariableResultSearchDto>> CachedCenterVariables(int centerVariableParentId)
        {
            var data = new List<CenterVariableResultSearchDto>();

            if (_memoryCaching.IsInCache($"{CacheKey}CenterVariableParentId={centerVariableParentId}"))
                data = await CacheCenterVariables(data, centerVariableParentId);
            else
            {
                data = await GetCenterVariablesByParentId(centerVariableParentId);
                _ = CacheCenterVariables(data, centerVariableParentId);
            }

            return data;
        }
        private Task<List<CenterVariableResultSearchDto>> CacheCenterVariables(List<CenterVariableResultSearchDto> data, int centerVariableParentId)
        {
            return _memoryCaching.GetOrCreateAsync<List<CenterVariableResultSearchDto>>($"{CacheKey}CenterVariableParentId={centerVariableParentId}",
                               () => Task.FromResult(data), TimeSpan.FromMinutes(_siteSettings.Value.DataCacheTimeInMinute));
        }

        public async Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentAndInternalUsage(int internalUsage, int centerVariableParentId)
        {
            var centerVariables = _context.CenterVariables.Where(q =>
                           centerVariableParentId == 0 ? true : q.ParentId == centerVariableParentId);

            centerVariables = FilterCenterActivitiesByInternalUsage(centerVariables, internalUsage);

            var result = await centerVariables
                .Select(s => new CenterVariableResultSearchDto
                {
                    ParentId = s.ParentId,
                    CenterVariableId = s.Id,
                    Code = s.Code,
                    EnName = s.EnName,
                    InternalUsage = s.InternalUsage,
                    Name = s.Name,
                    Sort = s.Sort,
                    Address = s.Address,
                    Icon = s.Icon,
                    ShortKey = s.ShortKey
                }).ToListAsync();

            foreach (var item in result)
                item.InternalUsageString = ((InternalUsage)item.InternalUsage).DisplayName();

            return result.OrderBy(o => o.Sort).ToList();
        }

        private IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> FilterCenterActivitiesByInternalUsage(IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables, int internalUsage)
        {
            return _internalUsageRules.InternalUsageRule[(InternalUsage)internalUsage](centerVariables);
        }

        private IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> FilterInternalUsageByTenant(IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables, int tenantId)
        {
            return _internalUsageRules.InternalUsageByTenantRule[(byte)Tenants.EpdPayvand == tenantId](centerVariables);
        }

        public Task<List<ActivityDto>> GetActivitiesByCenterId(int centerId)
        {
            var res = from a in _context.Activities
                      join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                      where a.CenterId == centerId
                      select new ActivityDto
                      {
                          CenterId = a.CenterId,
                          CenterVariableId = a.CenterVariableId,
                          Id = a.Id,
                          ValidFromPersian = a.ValidFrom.ToShamsiDateWithPersianNumber(),
                          ValidToPersian = a.ValidTo.ToShamsiDateWithPersianNumber(),
                          ValidFrom = (a.ValidFrom),
                          ValidTo = (a.ValidTo),
                          CenterVariableName = cv.Name,
                          ParentId = cv.ParentId
                      };

            return res.ToListAsync();
        }
        public Task<List<ActivityDto>> GetActivitiesByTenantId(int tenantId, int parentId)
        {
            var res = from a in _context.Activities
                      join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                      where a.TenantId == tenantId &&
                              a.ValidFrom <= DateTimeOffset.UtcNow && a.ValidTo >= DateTimeOffset.UtcNow
                            && cv.ParentId == parentId
                      select new ActivityDto
                      {
                          Id = a.Id,
                          CenterId = a.CenterId,
                          ValidFrom = (a.ValidFrom),
                          ValidTo = (a.ValidTo),
                          CenterVariableName = cv.Name,
                          ParentId = cv.ParentId,
                          CenterVariableId = a.CenterVariableId
                      };

            return res.ToListAsync();
        }
        public async Task<List<ActivityParentDto>> GetActivitiesParentByCenterId(int centerId)
        {
            var res = from a in _context.Activities
                      join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                      join cvParent in _context.CenterVariables on cv.ParentId equals cvParent.Id
                      where a.CenterId == centerId
                      select new ActivityParentDto
                      {
                          CenterId = a.CenterId,
                          CenterVariableId = cvParent.Id,
                          CenterVariableName = cvParent.Name,
                      };

            return await res.Distinct().ToListAsync();
        }
        public async Task<List<ActivityDto>> GetActivitiesByParentAndCenterId(int centerId, int parentId)
        {
            var res = from a in _context.Activities
                      join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                      where a.CenterId == centerId && cv.ParentId == parentId
                      select new ActivityDto
                      {
                          CenterId = a.CenterId,
                          CenterVariableId = a.CenterVariableId,
                          Id = a.Id,
                          ValidFromPersian = a.ValidFrom.ToShamsiDateWithPersianNumber(),
                          ValidToPersian = a.ValidTo.ToShamsiDateWithPersianNumber(),
                          ValidFrom = (a.ValidFrom),
                          ValidTo = (a.ValidTo),
                          CenterVariableName = cv.Name,
                          ParentId = cv.ParentId
                      };

            return await res.Distinct().ToListAsync();
        }

        public Task<List<ApplicationDto>> GetActiveApplications(int tenantId)
        {
            var query = from a in _context.Activities
                        join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                        join c in _context.Centers on a.CenterId equals c.Id
                        where c.TenantId == tenantId
                        && a.ValidFrom <= DateTime.UtcNow
                        && (a.ValidTo == (DateTimeOffset?)null || a.ValidTo >= DateTime.UtcNow)
                        orderby cv.ParentId, cv.Sort
                        select new ApplicationDto
                        {
                            Code = cv.Code,
                            CenterId = c.Id,
                            CenterVariableId = cv.Id,
                            ActivityId = a.Id,
                            TenantId = c.TenantId,
                            ValidFrom = a.ValidFrom,
                            ValidTo = a.ValidTo,
                            Address = cv.Address,
                            CenterVariableEnName = cv.EnName,
                            Icon = cv.Icon,
                            ShortKey = cv.ShortKey,
                            Sort = cv.Sort,
                            CenterVariableName = cv.Name,
                            CenterVariableParentId = cv.ParentId
                        };

            return query.ToListAsync();
        }

        public async Task<List<ApplicationDto>> GetActiveCloudApplications(int tenantId)
        {
            var query = from a in _context.Activities
                        join cv in _context.CenterVariables on a.CenterVariableId equals cv.Id
                        join c in _context.Centers on a.CenterId equals c.Id
                        where c.TenantId == tenantId
                        && a.ValidFrom <= DateTime.UtcNow
                        && (a.ValidTo == (DateTimeOffset?)null || a.ValidTo >= DateTime.UtcNow)
                        && (cv.ParentId == (byte)CenterVariableType.HIS_Cloud || cv.ParentId == (byte)CenterVariableType.Under_Cloud)
                        orderby cv.ParentId, cv.Sort
                        select new ApplicationDto
                        {
                            Code = cv.Code,
                            CenterId = c.Id,
                            CenterVariableId = cv.Id,
                            ActivityId = a.Id,
                            TenantId = c.TenantId,
                            ValidFrom = a.ValidFrom,
                            ValidTo = a.ValidTo,
                            Address = cv.Address,
                            CenterVariableEnName = cv.EnName,
                            Icon = cv.Icon,
                            ShortKey = cv.ShortKey,
                            Sort = cv.Sort,
                            CenterVariableName = cv.Name,
                            CenterVariableParentId = cv.ParentId
                        };

            return await query.ToListAsync();
        }
        public async Task<List<ApplicationDto>> GetCenterApplications(int tenantId)
        {
            var query = from a in _context.Activities.IgnoreQueryFilters()
                        join cv in _context.CenterVariables.IgnoreQueryFilters() on a.CenterVariableId equals cv.Id
                        join c in _context.Centers.IgnoreQueryFilters() on a.CenterId equals c.Id
                        where c.TenantId == tenantId
                        && (cv.ParentId == (byte)CenterVariableType.HIS_Cloud || cv.ParentId == (byte)CenterVariableType.Under_Cloud
                        || cv.ParentId == (byte)CenterVariableType.Windows_Base || cv.ParentId == (byte)CenterVariableType.HIS_Web)
                        orderby cv.ParentId, cv.Sort
                        select new ApplicationDto
                        {
                            Code = cv.Code,
                            CenterId = c.Id,
                            CenterVariableId = cv.Id,
                            ActivityId = a.Id,
                            TenantId = c.TenantId,
                            ValidFrom = a.ValidFrom,
                            ValidTo = a.ValidTo,
                            Address = cv.Address,
                            CenterVariableEnName = cv.EnName,
                            Icon = cv.Icon,
                            ShortKey = cv.ShortKey,
                            Sort = cv.Sort,
                            CenterVariableName = cv.Name,
                            Status = a.Status != 0 ? a.Status : cv.Status,
                            CenterVariableParentId = cv.ParentId
                        };

            return await query.ToListAsync();
        }

        public Task<List<CenterVariableResultSearchDto>> GetActiveApplicationsByInernalUsage(int tenantId, int appType)
        {
            var query = from cv in _context.CenterVariables.Include(p => p.Parent)
                        join a in _context.Activities on cv.Id equals a.CenterVariableId
                        join c in _context.Centers on a.CenterId equals c.Id
                        where c.TenantId == tenantId &&
                        (cv.ParentId == (int)CenterVariableType.Applications ||
                        cv.Parent.ParentId == (int)CenterVariableType.Applications)
                        select cv;

            query = FilterInternalUsageByTenant(query, tenantId);

            var result = (from cv in query
                          select new CenterVariableResultSearchDto
                          {
                              CenterVariableId = cv.Id,
                              Name = cv.Name,
                              ParentId = cv.ParentId,
                              EnName = cv.EnName,
                              Code = cv.Code,
                              InternalUsage = cv.InternalUsage,
                              InternalUsageString = ((InternalUsage)cv.InternalUsage).DisplayName(),
                              Sort = cv.Sort,
                              Address = cv.Address,
                              Icon = cv.Icon,
                              ShortKey = cv.ShortKey
                          }).ToListAsync();
            return result;
        }

        public async Task<List<CenterVariablesWithActiveApplicationsDto>> GetCenterVariablesWithActiveApplications(int parentId, int centerId, int tenantId)
        {
            var query = from centerVariable in _context.CenterVariables
                        join c1 in _context.CenterVariables on centerVariable.Id equals c1.Id
                        where c1.ParentId == parentId
                        select centerVariable;

            var centerVariables = await FilterInternalUsageByTenant(query, tenantId)
                    .Select(x => new CenterVariablesWithActiveApplicationsDto
                    {
                        CenterId = centerId,
                        TenantId = tenantId,
                        CenterVariableId = x.Id,
                        IsAssined = false,
                        CenterVariableName = x.Name
                    }).ToListAsync();

            var activities = await GetActivitiesByCenterId(centerId);

            centerVariables = SetAssignedActivities(centerVariables, activities);

            return centerVariables;
        }



        private List<CenterVariablesWithActiveApplicationsDto> SetAssignedActivities(List<CenterVariablesWithActiveApplicationsDto> centerVariables, List<ActivityDto> activities)
        {
            foreach (var centerVariable in centerVariables)
                if (activities.Where(x => x.CenterVariableId == centerVariable.CenterVariableId).Any())
                {
                    centerVariable.ActivityId = activities.FirstOrDefault(x => x.CenterVariableId == centerVariable.CenterVariableId).Id;
                    centerVariable.IsAssined = true;
                }

            return centerVariables;
        }
        public async Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentId(int centerVariableParentId)
        {
            var centerVariables = _context.CenterVariables.AsNoTracking()
                .Where(q => centerVariableParentId == 0 ? true : q.ParentId == centerVariableParentId);

            var result = await centerVariables
                          .Select(s => new CenterVariableResultSearchDto
                          {
                              ParentId = s.ParentId,
                              CenterVariableId = s.Id,
                              Code = s.Code,
                              EnName = s.EnName,
                              InternalUsage = s.InternalUsage,
                              Name = s.Name,
                              Sort = s.Sort,
                              Address = s.Address,
                              ShortKey = s.ShortKey,
                              Icon = s.Icon
                          }).ToListAsync();

            foreach (var item in result)
            {
                var internalUsageEnum = (InternalUsage)item.InternalUsage;
                item.InternalUsageString = internalUsageEnum.DisplayName();
            }

            return result.OrderBy(x => x.Sort).ToList();
        }
        public async Task<List<CenterVariableCodeDto>> GetApplications()
        {
            return await (from s in _context.CenterVariables
                          where s.Code > 0
                          orderby s.ParentId, s.Sort
                          select new CenterVariableCodeDto
                          {
                              ParentId = s.ParentId,
                              CenterVariableId = s.Id,
                              Code = s.Code,
                              EnName = s.EnName,
                              Name = s.Name,
                          }).ToListAsync();
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithParentByParentId(int centerVariableParentId)
        {
            var centerVariables = _context.CenterVariables.Where(q =>
                   centerVariableParentId == 0 ? true : q.ParentId == centerVariableParentId || q.Id == centerVariableParentId);

            var result = centerVariables
                          .Select(s => new CenterVariableResultSearchDto
                          {
                              ParentId = s.ParentId,
                              CenterVariableId = s.Id,
                              Code = s.Code,
                              EnName = s.EnName,
                              InternalUsage = s.InternalUsage,
                              Name = s.Name,
                              Sort = s.Sort,
                              Address = s.Address,
                              ShortKey = s.ShortKey,
                              Icon = s.Icon
                          });

            //foreach (var item in result)
            //{
            //    var internalUsageEnum = (InternalUsage)item.InternalUsage;
            //    item.InternalUsageString = internalUsageEnum.DisplayName();
            //}

            return result.OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithChildrenByParentId(int centerVariableParentId)
        {
            var centerVariables = _context.CenterVariables.Where(q =>
                   centerVariableParentId == 0 ? true : q.ParentId == centerVariableParentId || q.Id == centerVariableParentId || q.Parent.ParentId == centerVariableParentId);

            var result = await centerVariables
                          .Select(s => new CenterVariableResultSearchDto
                          {
                              ParentId = s.ParentId,
                              CenterVariableId = s.Id,
                              Code = s.Code,
                              EnName = s.EnName,
                              InternalUsage = s.InternalUsage,
                              Name = s.Name,
                              Sort = s.Sort,
                              Address = s.Address,
                              ShortKey = s.ShortKey,
                              Icon = s.Icon
                          })
                          .ToListAsync();

            foreach (var item in result)
            {
                var internalUsageEnum = (InternalUsage)item.InternalUsage;
                item.InternalUsageString = internalUsageEnum.DisplayName();
            }

            return result.OrderBy(x => x.Sort).ToList();
        }


        #endregion
    }
}