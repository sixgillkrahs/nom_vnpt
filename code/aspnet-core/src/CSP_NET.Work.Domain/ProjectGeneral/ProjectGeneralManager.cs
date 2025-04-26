using CSP_NET.Work.ProjectRole;
using CTIN.Abp;
using CTIN.Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectGenerals
{ 
    public class ProjectGeneralManager : DomainService
    {
        private readonly IProjectGeneralRepository _projectGeneralRepository;
        public ProjectGeneralManager(IProjectGeneralRepository projectGeneralRepository) {
            _projectGeneralRepository = projectGeneralRepository;
        }

        public async Task<ProjectGeneral> CreateAsync(
        [NotNull] string Code, [NotNull] string Name, Guid DepartmentID, string Description, Guid? ProjectStateID, bool Status)
        {
            Check.NotNullOrWhiteSpace(Code, nameof(Code));
            Check.NotNullOrWhiteSpace(Name, nameof(Name));

            var existingRole = await _projectGeneralRepository.FindByCodeAsync(Code);
            if (existingRole != null)
            {
                throw new UserFriendlyException("Ma du an da ton tai");
            }

            existingRole = await _projectGeneralRepository.FindByNameAsync(Name);
            if (existingRole != null)
            {
                throw new UserFriendlyException("Ten du an da ton tai");
            }

            return new ProjectGeneral(
                GuidGenerator.Create(),
                Code,
                Name,
                DepartmentID,
                Description,
                ProjectStateID,
                Status
            );
        }
        public async Task ChangeCodeAsync(
         [NotNull] ProjectGeneral project,
         [NotNull] string code)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var existingOrigin = await _projectGeneralRepository.FindByCodeAsync(code);
            if (existingOrigin != null && existingOrigin.Id != project.Id)
            {
                throw new UserFriendlyException("ma du an da ton tai");
            }

            project.ChangeCode(code);
        }

        public async Task ChangeNameAsync(
            [NotNull] ProjectGeneral project,
            [NotNull] string name)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingOrigin = await _projectGeneralRepository.FindByNameAsync(name);
            if (existingOrigin != null && existingOrigin.Id != project.Id)
            {
                throw new UserFriendlyException("ten du an da ton tai");
            }
            project.ChangeName(name);
        }


    }
 }

