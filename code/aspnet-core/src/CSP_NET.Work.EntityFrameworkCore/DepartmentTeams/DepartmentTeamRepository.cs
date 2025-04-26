using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CSP_NET.Work.DepartmentTeams;

public class DepartmentTeamRepository : EfCoreRepository<WorkDbContext, DepartmentTeam, Guid>, IDepartmentTeamRepository
{
    public DepartmentTeamRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<DepartmentTeam> FindByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<DepartmentTeam> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        // return await dbSet.Include("Members.Member").FirstOrDefaultAsync(x=> x.Id == id);
        return await dbSet.Include("Members.Member").Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DepartmentTeam> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<DepartmentTeam>> GetListAllAsync(string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        // return await dbSet.Include("Members.Member").FirstOrDefaultAsync(x=> x.Id == id);
        return await dbSet.Include("Members.Member").Include(x => x.Department).ToListAsync();
    }

    public async Task<List<DepartmentTeam>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        throw new NotImplementedException();
    }

    public override async Task<IQueryable<DepartmentTeam>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
