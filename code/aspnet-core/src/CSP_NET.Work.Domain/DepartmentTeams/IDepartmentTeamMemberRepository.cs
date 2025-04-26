//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using CTIN.Abp.Domain.Repositories;

//namespace CSP_NET.Work.DepartmentTeamMembers;

//public interface IDepartmentTeamMemberRepository : IRepository<DepartmentTeamMember, Guid>
//{
//    Task<DepartmentTeam_Members> FindByDepartmentAsync(Guid departmentID);
//    Task<List<DepartmentTeamMember>> GetListAsync(
//        int skipCount,
//        int maxResultCount,
//        string sorting,
//        string filter = null
//    );
//    Task<List<DepartmentTeamMember>> GetListAllAsync(
//       string sorting,
//       string filter = null
//   );
//}
