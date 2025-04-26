using AutoMapper.Internal.Mappers;
using CSP_NET.Work.ProjectRole;
using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.BackgroundWorkers.Hangfire;
using CTIN.Abp.Data;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.Guids;
using CTIN.Abp.Identity;
using CTIN.Abp.MultiTenancy;
using CTIN.Abp.ObjectMapping;
using CTIN.Abp.TenantManagement;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CTIN.Abp.Identity.IdentityPermissions;
using static CTIN.Abp.TenantManagement.TenantManagementPermissions;
using CSP_NET.Work.Common;
using Namotion.Reflection;
using System.Globalization;
namespace CSP_NET.Work.SyncCtin
{
    public class SyncCtinDeparmentWorker : HangfireBackgroundWorkerBase
    {

        private readonly IHRMCTIN_HTTPClient _idsHTTPClient;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IRepository<OrganizationUnitType, Guid> _organizationUnitTypeRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;
        private readonly IDataFilter _dataFilter;
        private const string BASE_TYPE = "BaseType";
        public SyncCtinDeparmentWorker(IGuidGenerator guidGenerator,
            IHRMCTIN_HTTPClient idsHTTPClient,
            OrganizationUnitManager organizationUnitManager,
            IOrganizationUnitRepository organizationUnitRepository,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant,
            IDataFilter dataFilter,
            IRepository<OrganizationUnitType, Guid> organizationUnitTypeRepository) : base()
        {

            RecurringJobId = nameof(SyncCtinDeparmentWorker);
            TimeZone = TimeZoneInfo.Local;
            CronExpression = Cron.Daily();
            Queue = WorkConsts.BackgroundWorkerQueue;
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _idsHTTPClient = idsHTTPClient;
            _guidGenerator = guidGenerator;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;
            _dataFilter = dataFilter;
            _organizationUnitTypeRepository = organizationUnitTypeRepository;

        }

        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogInformation("Executed SyncCtinDepartment..!");
            try
            {
                var organizationUnits = await _idsHTTPClient.GetDepartent();
                var organizationUnitsASC = organizationUnits.OrderBy(x => x.typeId).ToList();


                

                foreach (var companyInfo in organizationUnitsASC.Where(a => a.parentId == null))
                {
                    await ProcessCompanyInfoAsync(organizationUnitsASC, companyInfo);
                }

                Logger.LogInformation("Done SyncCtinDepartment!");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while processing SyncCtinUser");
            }
        }


        private async Task<Guid> InsertOrganizationUnitAsync(OrganizationUnit organizationUnit)
        {
            await _organizationUnitRepository.InsertAsync(organizationUnit);

            // Lấy giá trị GUID sau khi chèn
            return organizationUnit.Id;
        }
        private async Task ProcessCompanyInfoAsync(List<CompanyInfo> organizationUnitsASC, CompanyInfo companyInfo)
        {
            try
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    Guid? tenanid = companyInfo.TenantId;
                    var OrganizationUnitAll = await _organizationUnitRepository.GetListAsync();
                    var existingOrganizationUnit = OrganizationUnitAll.Where(x => x.BusinessCode == companyInfo.companyCode && x.TenantId == tenanid).FirstOrDefault();


                    // Bước 1: Duyệt từng bản ghi
                    Guid parentGuid = SyncCommon.Int2Guid(companyInfo.id);
                    if (existingOrganizationUnit == null)
                    {
                        // Thêm mới đơn vị
                        var typeOrgId = await GetUnitTypeId(companyInfo.typeId.GetValueOrDefault(), tenanid);


                        var organizationUnit = new OrganizationUnit(
                            parentGuid,
                            companyInfo.companyName,
                            typeOrgId,
                            companyInfo.companyCode,
                            companyInfo.data.taxCode,
                            companyInfo.dataContact.phone,
                            null,
                            tenanid
                        );

                        await _organizationUnitManager.CreateAsync(organizationUnit);
                    }
                    else
                    {
                        await UpdateOrganizationUnit(existingOrganizationUnit, companyInfo);
                    }
                    // Bước 3: Thêm mới và tìm tất cả các con của bản ghi đó
                    await InsertOrUpdateOrganizationUnit(organizationUnitsASC, companyInfo.id, parentGuid);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"An error occurred while processing companyInfo: {companyInfo}");
            }
        }

        private async Task InsertOrUpdateOrganizationUnit(List<CompanyInfo> organizationUnits, int parentId, Guid parentGuid)
        {
            try
            {
                var OrganizationUnitAll = await _organizationUnitRepository.GetListAsync();
                // Tìm tất cả các con của bản ghi đó
                var childOrganization = organizationUnits.Where(x => x.parentId == parentId).ToList();
                foreach (var unit in childOrganization)
                {
                    // Kiểm tra dữ liệu trùng lặp trước khi thêm mới
                    var existingChild = OrganizationUnitAll.Where(x => x.BusinessCode == unit.companyCode).FirstOrDefault();

                    Guid curentGuid = SyncCommon.Int2Guid(unit.id);

                    // Thêm mới đơn vị
                    if (existingChild == null)
                    {
                        Guid? tenanid = unit.TenantId;
                        var typeOrgId = await GetUnitTypeId(unit.typeId.GetValueOrDefault(), tenanid);
                        var organizationUnit = new OrganizationUnit(
                            curentGuid,
                            unit.companyName,
                            typeOrgId,
                            unit.companyCode,
                            unit.data.taxCode,
                            unit.dataContact.phone,
                            parentGuid,
                            tenanid
                        );

                        await _organizationUnitManager.CreateAsync(organizationUnit);
                    }
                    else
                    {
                        // Đơn vị đã tồn tại, cập nhật thông tin
                        await UpdateOrganizationUnit(existingChild, unit);
                    }
                    await InsertOrUpdateOrganizationUnit(organizationUnits, unit.id, curentGuid);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"An error occurred while processing companyInfo: {parentGuid}");
            }

        }
        private async Task UpdateOrganizationUnit(OrganizationUnit organizationUnit, CompanyInfo companyInfo)
        {
            organizationUnit.DisplayName = companyInfo.companyName;
            organizationUnit.BusinessCode = companyInfo.companyCode;
            organizationUnit.TaxCode = companyInfo.data.taxCode;
            organizationUnit.PhoneNumber = companyInfo.dataContact.phone;
            await _organizationUnitManager.UpdateAsync(organizationUnit);
        }

        private async Task<Guid> GetUnitTypeId(int typeId, Guid? tenantId)
        {

            var query = await _organizationUnitTypeRepository.GetQueryableAsync();
            var organizationUnitType = query.FirstOrDefault(a => a.TryGetPropertyValue<int>(BASE_TYPE, -1) == typeId && a.TenantId == tenantId);
            if( organizationUnitType == null )
            {
                
                organizationUnitType = new OrganizationUnitType(_guidGenerator.Create(),EnumDesc.EnumCtinOgranizationName(typeId), tenantId) { IsActive = true };
                organizationUnitType.SetProperty(BASE_TYPE, typeId);
                await _organizationUnitTypeRepository.InsertAsync(organizationUnitType, true);
            }
            return organizationUnitType.Id;
        }
    }
}
