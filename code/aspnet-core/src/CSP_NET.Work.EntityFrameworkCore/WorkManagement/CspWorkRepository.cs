using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CSP_NET.Work.Common;
using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using CTIN.Abp.Validation.StringValues;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading;

namespace CSP_NET.Work.WorkManagement;

public class CspWorkRepository : EfCoreRepository<WorkDbContext, CspWork, Guid>, ICspWorkRepository
{
    public CspWorkRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<CspWork>> GetChildrenAsync(
        Guid? parentId,
        bool recursive = false,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = query.AsNoTracking();

        if (!parentId.HasValue)
        {
            return await query
                .OrderBy(x => x.Code)
                .ToListAsync(cancellationToken);
        }

        if (!recursive)
        {
            return await query
                .Where(x => x.ParentId == parentId)
                .OrderBy(x => x.Code)
                .ToListAsync();
        }


        var code = (await query.FirstOrDefaultAsync(x => x.Id == parentId.Value)).Code;

        return await query
            .Where(x => x.Code.StartsWith(code) && x.Id != parentId)
            .OrderBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CspWork>> GetListAsync(
        string sorting,
        string? keyword,
        string? workCode,
        string? taskCode,
        string? priority,
        Guid? projectId,
        Guid? parentId,
        CspWorkStatus? Status,
        Guid? ownerId,
        Guid? assignerId,
        Guid? performerId,
        Guid? collaboratorId,
        Guid? currUserId,
        int? skipCount = 0,
        int? maxResultCount = int.MaxValue,
        bool onlyRootNode = true,
        bool include = false,
        bool hasChildren = false,
        CancellationToken cancellationToken = default)
    {


        var query = await CreateFilteredQueryAsync(
                          keyword,
                          workCode,
                          taskCode,
                          priority,
                          projectId,
                          parentId,
                          Status,
                          ownerId,
                          assignerId,
                          performerId,
                          collaboratorId,
                          currUserId,
                          onlyRootNode,
                          include,
                          hasChildren);
        return await query
             .OrderBy(sorting ?? $"{nameof(CspWork.Code)}")
             .Skip(skipCount ?? 0)
             .Take(maxResultCount ?? int.MaxValue)
             .ToListAsync(cancellationToken);
    }
    public async Task<IQueryable<CspWork>> CreateFilteredQueryAsync(
       string? keyword,
       string? workCode,
       string? taskCode,
       string? priority,
       Guid? projectId,
       Guid? parentId,
       CspWorkStatus? Status,
       Guid? ownerId,
       Guid? assignerId,
       Guid? performerId,
       Guid? collaboratorId,
       Guid? currUserId,
       bool onlyRootNode = true,
       bool include = false,
       bool hasChildren = false)
    {

        string filter = keyword.IsNullOrEmpty() ? string.Empty : keyword.ToUpper();
        var query = await GetQueryableAsync();
        return query
             .IncludeDetails(include, hasChildren)
             .WhereIf(currUserId.HasValue, x => x.Members.Contains(currUserId.Value.ToString()))
             .WhereIf(!filter.IsNullOrEmpty(),
              x => x.NomalizedName.Contains(filter) ||
              x.NomalizedTaskCode.Contains(filter) ||
              x.NomalizedWorkCode.Contains(filter))
             .WhereIf(!workCode.IsNullOrEmpty(), x => x.WorkCode == workCode)
             .WhereIf(!taskCode.IsNullOrEmpty(), x => x.TaskCode == taskCode)
             .WhereIf(!priority.IsNullOrEmpty(), x => x.Priority == priority)
             .WhereIf(projectId != null, x => x.ProjectId == projectId)
             .WhereIf(Status != null, x => x.Status == Status)
             .WhereIf(ownerId.HasValue || assignerId.HasValue || performerId.HasValue || collaboratorId.HasValue,
             x => x.Owner == ownerId || x.Assigner == assignerId || x.Performer == performerId || x.Collaborators.Contains(collaboratorId.ToString()))
             .WhereIf(onlyRootNode, x => x.ParentId == null)
             .WhereIf(parentId != null, x => x.ParentId == parentId);
    }

    public async Task<List<CspWork>> GetParentAsync(
        string code,
        int level, Guid parentId,
        bool hasFamily = false,
        bool recursive = false,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = query
                .AsNoTracking()
                .Where(x => x.Code != code);
        if (!recursive)
        {
            if (hasFamily)
                return await query
                     .Where(x => x.Id == parentId || x.ParentId == parentId)
                     .OrderBy(x => x.Code)
                     .ToListAsync(cancellationToken);
            else
                return await query.Where(x => x.Id == parentId).ToListAsync(cancellationToken);
        }
        var splitCode = code.Split(".");
        return await query
        .Where(x =>
               x.Code.StartsWith(splitCode[0])
               && ((hasFamily && x.Level <= level) || (!hasFamily && x.Level < level)))
        .OrderBy(x => x.Code)
        .ToListAsync(cancellationToken);
    }

    public override async Task<IQueryable<CspWork>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    public async Task<int> GetCountAsync(
        string? keyword,
        string? workCode,
        string? taskCode,
        string? priority,
        Guid? projectId,
        Guid? parentId,
        CspWorkStatus? Status,
        Guid? ownerId,
        Guid? assignerId,
        Guid? performerId,
        Guid? collaboratorId,
        Guid? currUserId,
        bool onlyRootNode = true)
    {
        var query = await CreateFilteredQueryAsync(
                                keyword,
                                workCode,
                                taskCode,
                                priority,
                                projectId,
                                parentId,
                                Status,
                                ownerId,
                                assignerId,
                                performerId,
                                collaboratorId,
                                currUserId,
                                onlyRootNode);
        return await query.CountAsync();
    }

    public async Task<List<CspWork>> GetListAllAsync(string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                obj => obj.Members.Contains(filter)
             ).OrderBy(sorting)
            .ToListAsync();
    }

    public async Task<List<CspWork>> GetListAllWork(List <Guid> Ids)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(w=>Ids.Contains(w.Id))
            .ToListAsync();
    }
}
