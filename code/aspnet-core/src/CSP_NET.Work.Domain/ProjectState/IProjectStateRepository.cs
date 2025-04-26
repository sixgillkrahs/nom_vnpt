using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTIN.Abp.Domain.Repositories;

namespace CSP_NET.Work.ProjectState;

public interface IProjectStateRepository : IRepository<ProjectState, Guid>
{
    Task<ProjectState> FindByCodeAsync(string code);
    Task<ProjectState> FindByNameAsync(string name);
    Task<ProjectState> FindByIdAsync(Guid id);
    Task<List<ProjectState>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    Task<List<ProjectState>> GetListAllAsync(
       string sorting,
       string filter = null
   );
}
