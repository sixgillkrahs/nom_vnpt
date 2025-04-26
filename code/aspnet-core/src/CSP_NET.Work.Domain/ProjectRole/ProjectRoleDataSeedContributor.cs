using CTIN.Abp.Data;
using CTIN.Abp.DependencyInjection;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.Guids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectRole
{
    public class ProjectRoleDataSeedContributor : ITransientDependency
    {
        private readonly IRepository<ProjectRole, Guid> _projectRoleRepository;
        private readonly IGuidGenerator _guidGenerator;

        public ProjectRoleDataSeedContributor(IGuidGenerator guidGenerator, IRepository<ProjectRole, Guid> projectRoleRepository)
        {
            _guidGenerator = guidGenerator;
            _projectRoleRepository = projectRoleRepository;
        }

        //Task IDataSeedContributor.SeedAsync(DataSeedContext context)
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public async Task SeedAsync(DataSeedContext context)
    //{
        //if (await _projectRoleRepository.GetCountAsync() > 0)
        //{
        //    return;
        //}

        //var menu = await _projectRoleRepository.InsertAsync(new ProjectRole()
        //{
        //    ClientId = "Work_App",
        //    IconClass = "pi pi-sliders-h",
        //    IsGroup = true,
        //    Label = "Menu",
        //    Order = 1
        //});

        //await _projectRoleRepository.InsertAsync(new ProjectRole()
        //{
        //    ClientId = "Work_App",
        //    IconClass = "pi pi-arrows-alt",
        //    IsGroup = false,
        //    Label = "Quản lí menu",
        //    Order = 1,
        //    RouterLink = "/menu-management",
        //    ParentId = menu.Id
        //});
    //}
}
