using CTIN.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectRole
{
    public interface IProjectRoleRepository : IRepository<ProjectRole, Guid>
    {
        Task<ProjectRole> FindByCodeAsync(string code);
        Task<ProjectRole> FindByNameAsync(string name);
        Task<ProjectRole> FindByIdAsync(Guid id);
        Task<List<ProjectRole>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
        Task<List<ProjectRole>> GetListAllAsync(
           string sorting,
           string filter = null
       );
    }
}
