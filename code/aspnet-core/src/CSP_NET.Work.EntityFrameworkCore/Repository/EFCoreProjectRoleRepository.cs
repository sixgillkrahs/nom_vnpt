using CSP_NET.Work.EntityFrameworkCore;
using CSP_NET.Work.ProjectRole;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CSP_NET.Work.Repository
{
    public class EFCoreProjectRoleRepository : EfCoreRepository<WorkDbContext, ProjectRole.ProjectRole, Guid>,
             IProjectRoleRepository
    {
        public EFCoreProjectRoleRepository(
               IDbContextProvider<WorkDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
        }
        public async Task<ProjectRole.ProjectRole> FindByCodeAsync(string code)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Code == code);
        }


        public async Task<ProjectRole.ProjectRole> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProjectRole.ProjectRole> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<ProjectRole.ProjectRole>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
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
        public async Task<List<ProjectRole.ProjectRole>> GetListAllAsync(string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    obj => obj.Name.Contains(filter) || obj.Code.Contains(filter)
                 ).OrderBy(sorting)
                .ToListAsync();
        }
    }

}

