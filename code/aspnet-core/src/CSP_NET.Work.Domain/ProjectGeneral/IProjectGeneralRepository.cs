using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTIN.Abp.Domain.Repositories;

namespace CSP_NET.Work.ProjectGenerals;

public interface IProjectGeneralRepository : IRepository<ProjectGeneral, Guid>
{
    Task<ProjectGeneral> FindByCodeAsync(string code);
    Task<ProjectGeneral> FindByNameAsync(string name);
    Task<ProjectGeneral> FindByIdAsync(Guid id);
    Task<List<ProjectGeneral>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null,
        bool includeDetails = true
    );
    Task<List<ProjectGeneral>> GetListAllAsync(
       string sorting,
       string filter = null
   );
}
