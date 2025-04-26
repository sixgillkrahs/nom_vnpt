using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CSP_NET.Work.ProjectState;

public class ProjectStateRepository : EfCoreRepository<WorkDbContext, ProjectState, Guid>, IProjectStateRepository
{
    public ProjectStateRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<ProjectState> FindByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Code == code);
    }


    public async Task<ProjectState> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProjectState> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<ProjectState>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                obj => obj.Name.Contains(filter) || obj.Code.Contains(filter)
             ).OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    public async Task<List<ProjectState>> GetListAllAsync(string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                obj => obj.Name.Contains(filter) || obj.Code.Contains(filter)
             ).OrderBy(sorting)
            .ToListAsync();
    }



    public override async Task<IQueryable<ProjectState>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
