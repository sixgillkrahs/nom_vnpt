using CTIN.Abp.Domain.Services;
using CTIN.Abp;
using CTIN.Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.DepartmentTeams
{
    public class DepartmentTeamManager : DomainService
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public DepartmentTeamManager(IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
        }

        public async Task<DepartmentTeam> CreateAsync(
      [NotNull] string Code, [NotNull] string Name, string Description, Guid DepartmentID, string Status)
        {
            Check.NotNullOrWhiteSpace(Code, nameof(Code));
            Check.NotNullOrWhiteSpace(Name, nameof(Name));

            var existingRole = await _departmentTeamRepository.FindByCodeAsync(Code);
            if (existingRole != null)
            {
                throw new UserFriendlyException("Mã của Team đã tồn tại!");
            }

            existingRole = await _departmentTeamRepository.FindByNameAsync(Name);
            if (existingRole != null)
            {
                throw new UserFriendlyException("Tên của Team đã tồn tại!");
            }

            return new DepartmentTeam(
                GuidGenerator.Create(),
                Code,
                Name,
                Description,
                DepartmentID,
                Status
            );
        }

       
        //public async Task ChangeCodeAsync(
        // [NotNull] ProjectRole projectRole,
        // [NotNull] string code)
        //{
        //    Check.NotNull(projectRole, nameof(projectRole));
        //    Check.NotNullOrWhiteSpace(code, nameof(code));

        //    var existingOrigin = await _projectRoleRepository.FindByCodeAsync(code);
        //    if (existingOrigin != null && existingOrigin.Id != projectRole.Id)
        //    {
        //        throw new UserFriendlyException("name của Origin đã tồn tại");
        //    }

        //    projectRole.ChangeCode(code);
        //}

        //public async Task ChangeNameAsync(
        //    [NotNull] ProjectRole projectRole,
        //    [NotNull] string name)
        //{
        //    Check.NotNull(projectRole, nameof(projectRole));
        //    Check.NotNullOrWhiteSpace(name, nameof(name));

        //    var existingOrigin = await _projectRoleRepository.FindByNameAsync(name);
        //    if (existingOrigin != null && existingOrigin.Id != projectRole.Id)
        //    {
        //        throw new UserFriendlyException("name của Origin đã tồn tại");
        //    }
        //    projectRole.ChangeName(name);
        //}
    }
}

