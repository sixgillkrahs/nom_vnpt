using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP_NET.Work.Common;
using CTIN.Abp.Data;
using CTIN.Abp.DependencyInjection;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.Guids;
using CTIN.Abp.Identity;

namespace CSP_NET.Work.Data;
public class OrganizationUnitTypeDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<OrganizationUnitType, Guid> _organizationUnitTypeRepository;
    private readonly IGuidGenerator _guidGenerator;

    public OrganizationUnitTypeDataSeedContributor(IGuidGenerator guidGenerator, IRepository<OrganizationUnitType, Guid> organizationUnitTypeRepository)
    {
        _guidGenerator = guidGenerator;
        _organizationUnitTypeRepository = organizationUnitTypeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _organizationUnitTypeRepository.GetCountAsync() > 0)
        {
            return;
        }
        //var company = new OrganizationUnitType(_guidGenerator.Create(), "Công ty") { IsActive = true };
        //company.SetProperty("BaseType", 1);

        //var branch = new OrganizationUnitType(_guidGenerator.Create(), "Chi nhánh") { IsActive = true };
        //branch.SetProperty("BaseType", 2);

        //var department = new OrganizationUnitType(_guidGenerator.Create(), "Phòng ban") { IsActive = true };
        //department.SetProperty("BaseType", 3);

        //var local = new OrganizationUnitType(_guidGenerator.Create(), "Bộ phận") { IsActive = true };
        //company.SetProperty("BaseType", 4);

        //await _organizationUnitTypeRepository.InsertAsync(company, true);
        //await _organizationUnitTypeRepository.InsertAsync(branch, true);
        //await _organizationUnitTypeRepository.InsertAsync(department, true);
        //await _organizationUnitTypeRepository.InsertAsync(local, true);
    }
}