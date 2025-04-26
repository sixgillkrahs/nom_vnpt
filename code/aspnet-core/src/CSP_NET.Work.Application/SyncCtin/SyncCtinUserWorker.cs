using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.BackgroundWorkers.Hangfire;
using CTIN.Abp.Data;
using CTIN.Abp.Guids;
using CTIN.Abp.Identity;
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
    public class SyncCtinUserWorker : HangfireBackgroundWorkerBase
    {
        private readonly IIDSCTIN_HTTPClient _idsHTTPClient;
        private readonly IHRMCTIN_HTTPClient _hrmHTTPClient;
        private readonly IIDSUAT_HTTPClient _idsuatHTTPClient;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IGuidGenerator _guidGenerator;

        public SyncCtinUserWorker(IIDSCTIN_HTTPClient idsHTTPClient, IHRMCTIN_HTTPClient hrmHTTPClient, IdentityUserManager identityUserManager, IGuidGenerator guidGenerator, IIDSUAT_HTTPClient idsuatHTTPClient) : base()
        {

            RecurringJobId = nameof(SyncCtinUserWorker);
            TimeZone = TimeZoneInfo.Local;
            CronExpression = Cron.Daily();
            Queue = WorkConsts.BackgroundWorkerQueue;

            _idsHTTPClient = idsHTTPClient;
            _hrmHTTPClient = hrmHTTPClient;
            _identityUserManager = identityUserManager;
            _guidGenerator = guidGenerator;
            _idsuatHTTPClient = idsuatHTTPClient;
        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogInformation("Executed SyncCtinUser..!");

            try
            {
                using (var uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin())
                {
                    var employees = await _hrmHTTPClient.GetEmployees();
                    var users = await _idsuatHTTPClient.GetUsers();

                    var joinedUsers = employees.Join(users, x => x.id, y => y.employeeId, (x, y) =>
                    {
                        x.userName = y.userName;
                        return x;
                    }).ToList();

                    joinedUsers.ForEach(u =>
                    {
                        if (u.userName != null)
                        {
                            var oldUser = _identityUserManager.FindByNameAsync(u.userName).Result;
                            if (oldUser == null)
                            {
                                var identityUser = new CTIN.Abp.Identity.IdentityUser(_guidGenerator.Create(), u.userName, u.contact.email);
                                identityUser.SetProperty("info", new UserInfomation()
                                {
                                    code = u.basic.code,
                                    fullName = u.basic.fullName,
                                    title = u.curentJob.title.text
                                });
                                _identityUserManager.CreateAsync(identityUser).Wait();
                                _identityUserManager.SetPhoneNumberAsync(identityUser, u.contact.phone).Wait();
                                _identityUserManager.SetOrganizationUnitsAsync(identityUser, SyncCommon.Int2Guid(u.curentJob.department.value)).Wait();
                            }

                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while processing SyncCtinUser");
            }

            Logger.LogInformation("Done SyncCtinUser!");
            // return Task.CompletedTask;
        }

        public class UserInfomation
        {
            public string code { get; set; }

            public string fullName { get; set; }

            public string title { get; set; }
        }
    }
}
