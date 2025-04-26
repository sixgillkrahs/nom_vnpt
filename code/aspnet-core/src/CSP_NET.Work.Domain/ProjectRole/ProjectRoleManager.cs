using CTIN.Abp;
using CTIN.Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectRole
{
    public class ProjectRoleManager : DomainService
    {
        private readonly IProjectRoleRepository _projectRoleRepository;
        public ProjectRoleManager(IProjectRoleRepository projectRoleRepository)
        {
            _projectRoleRepository = projectRoleRepository;
        }

        public async Task<ProjectRole> CreateAsync(
      [NotNull] string Code, [NotNull] string Name, string Domain, bool Status)
        {
            Check.NotNullOrWhiteSpace(Code, nameof(Code));
            Check.NotNullOrWhiteSpace(Name, nameof(Name));

            var existingRole = await _projectRoleRepository.FindByCodeAsync(Code);
            if (existingRole != null)
            {
                throw new UserFriendlyException("Code của ProjectRole đã tồn tại");
            }

            existingRole = await _projectRoleRepository.FindByNameAsync(Name);
            if (existingRole != null)
            {
                throw new UserFriendlyException("name của ProjectRole đã tồn tại");
            }

            return new ProjectRole(
                GuidGenerator.Create(),
                Code,
                Name,
                Domain,
                Status
            );
        }

        public async Task ChangeCodeAsync(
         [NotNull] ProjectRole projectRole,
         [NotNull] string code)
        {
            Check.NotNull(projectRole, nameof(projectRole));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var existingOrigin = await _projectRoleRepository.FindByCodeAsync(code);
            if (existingOrigin != null && existingOrigin.Id != projectRole.Id)
            {
                throw new UserFriendlyException("name đã tồn tại");
            }

            projectRole.ChangeCode(code);
        }

        public async Task ChangeNameAsync(
            [NotNull] ProjectRole projectRole,
            [NotNull] string name)
        {
            Check.NotNull(projectRole, nameof(projectRole));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingOrigin = await _projectRoleRepository.FindByNameAsync(name);
            if (existingOrigin != null && existingOrigin.Id != projectRole.Id)
            {
                throw new UserFriendlyException("name đã tồn tại");
            }
            projectRole.ChangeName(name);
        }
    }
}
