using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.BackgroundWorkers.Hangfire;
using CTIN.Abp.Data;
using CTIN.Abp.Guids;
using CTIN.Abp.Identity;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.Uow;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin
{
    public class SyncCtinTenantWorker : HangfireBackgroundWorkerBase
    {
        private readonly IIDSCTIN_HTTPClient _idsHTTPClient;
        private readonly IGuidGenerator _guidGenerator;
        private readonly TenantManager _manager;

        public SyncCtinTenantWorker(IIDSCTIN_HTTPClient idsHTTPClient, IGuidGenerator guidGenerator, TenantManager manager) : base()
        {

            RecurringJobId = nameof(SyncCtinTenantWorker);
            TimeZone = TimeZoneInfo.Local;
            CronExpression = Cron.Daily();
            Queue = WorkConsts.BackgroundWorkerQueue;

            _idsHTTPClient = idsHTTPClient;
            _guidGenerator = guidGenerator;
            _manager = manager;

        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogInformation("Executed SyncCtinTenant..!");

            try
            {
                var tenants = await _idsHTTPClient.GetTenants();

                using (var uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin())
                {

                    foreach (var item in tenants)
                    {
                        var tenant = _manager.CreateAsync(item.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while processing SyncCtinTenant");
            }

            Logger.LogInformation("Done SyncCtinTenant!");
            // return Task.CompletedTask;
        }
    }
}
