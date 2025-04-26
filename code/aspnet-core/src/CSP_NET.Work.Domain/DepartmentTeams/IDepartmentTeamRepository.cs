using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTIN.Abp.Domain.Repositories;

namespace CSP_NET.Work.DepartmentTeams;

public interface IDepartmentTeamRepository : IRepository<DepartmentTeam, Guid>
{
    Task<DepartmentTeam> FindByCodeAsync(string code);
    Task<DepartmentTeam> FindByNameAsync(string name);
    Task<DepartmentTeam> FindByIdAsync(Guid id);
    Task<List<DepartmentTeam>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    Task<List<DepartmentTeam>> GetListAllAsync(
       string sorting,
       string filter = null
   );
}
