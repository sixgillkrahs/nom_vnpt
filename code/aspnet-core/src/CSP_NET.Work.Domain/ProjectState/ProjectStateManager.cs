using CSP_NET.Work.ProjectGenerals;
using CTIN.Abp;
using CTIN.Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectState
{
    public class ProjectStateManager : DomainService
    {
        private readonly IProjectStateRepository _projectStateRepository;
        public ProjectStateManager(IProjectStateRepository projectStateRepository)
        {
            _projectStateRepository = projectStateRepository;
        }

        public async Task<ProjectState> CreateAsync(
        [NotNull] string Code, [NotNull] string Name)
        {
            Check.NotNullOrWhiteSpace(Code, nameof(Code));
            Check.NotNullOrWhiteSpace(Name, nameof(Name));

            var existing = await _projectStateRepository.FindByCodeAsync(Code);
            if (existing != null)
            {
                throw new UserFriendlyException("Ma giai doan du an da ton tai");
            }

            existing = await _projectStateRepository.FindByNameAsync(Name);
            if (existing != null)
            {
                throw new UserFriendlyException("Ten cua giai doan du an da ton tai");
            }

            return new ProjectState(
                GuidGenerator.Create(),
                Code,
                Name
            );
        }
        public async Task ChangeCodeAsync(
         [NotNull] ProjectState project,
         [NotNull] string code)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var existing = await _projectStateRepository.FindByCodeAsync(code);
            if (existing != null && existing.Id != project.Id)
            {
                throw new UserFriendlyException("ma giai doan du an da ton tai");
            }

            project.ChangeCode(code);
        }

        public async Task ChangeNameAsync(
            [NotNull] ProjectState project,
            [NotNull] string name)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existing = await _projectStateRepository.FindByNameAsync(name);
            if (existing != null && existing.Id != project.Id)
            {
                throw new UserFriendlyException("ten giai doan du an da ton tai");
            }
            project.ChangeName(name);
        }

    }
}
