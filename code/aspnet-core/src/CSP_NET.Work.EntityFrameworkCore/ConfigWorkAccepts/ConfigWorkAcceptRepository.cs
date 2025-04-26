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

namespace CSP_NET.Work.ConfigworkAccepts;

public class ConfigWorkAcceptRepository : EfCoreRepository<WorkDbContext, ConfigWorkAccept, Guid>, IConfigWorkAcceptRepository
{
    public ConfigWorkAcceptRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<ConfigWorkAccept> FindByWorkIdAsync(Guid WorkID)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.WorkId == WorkID);
    }

    public async Task<ConfigWorkAccept> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        // return await dbSet.Include("Members.Member").FirstOrDefaultAsync(x=> x.Id == id);
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }


    public override async Task<IQueryable<ConfigWorkAccept>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
