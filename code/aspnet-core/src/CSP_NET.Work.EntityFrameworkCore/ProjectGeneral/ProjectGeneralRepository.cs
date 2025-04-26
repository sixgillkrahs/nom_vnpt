using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CSP_NET.Work.ProjectGenerals;

public class ProjectGeneralRepository : EfCoreRepository<WorkDbContext, ProjectGeneral, Guid>, IProjectGeneralRepository
{
    public ProjectGeneralRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<ProjectGeneral> FindByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Code == code);
    }


    public async Task<ProjectGeneral> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProjectGeneral> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<ProjectGeneral>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null, bool includeDetails = true)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.IncludeIf(includeDetails, a => a.ProjectState)
            .IncludeIf(includeDetails, b => b.Department)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                obj => obj.Name.ToUpper().Contains(filter) || obj.Code.ToUpper().Contains(filter))
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    public async Task<List<ProjectGeneral>> GetListAllAsync(string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                obj => obj.Name.Contains(filter) || obj.Code.Contains(filter)
             ).OrderBy(sorting)
            .ToListAsync();
    }

    public override async Task<IQueryable<ProjectGeneral>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
