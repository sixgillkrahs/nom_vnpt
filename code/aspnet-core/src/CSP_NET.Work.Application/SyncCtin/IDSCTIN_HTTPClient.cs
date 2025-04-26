using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin
{
    public interface IIDSCTIN_HTTPClient
    {
        Task<List<CTINUser>> GetUsers();
        Task<List<TenantDTO>> GetTenants();
    }
    public class IDSCTIN_HTTPClient : BaseHttpClient, IIDSCTIN_HTTPClient, IScopedDependency
    {
        public IDSCTIN_HTTPClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory, SyncCtinConsts.IDS)
        {

        }

        public async Task<List<CTINUser>> GetUsers()
        {
            var result = new List<CTINUser>();
            for (var i = 1; i <= 10000; i++)
            {
                var res = await GetAsync<ResultModel2<List<CTINUser>>>($"api/Employees?PageNumber={i}&PageSize=100&where=%7B%22dataDb.status%22:%7B%22neq%22:3%7D%7D");
                if (res != null)
                {
                    result.AddRange(res?.data ?? new List<CTINUser>());
                    if (res.page.total == result.Count)
                        return result;
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
            return result;
        }

        public async Task<List<TenantDTO>> GetTenants()
        {
            var result = new List<TenantDTO>();
            for (var i = 1; i <= 10000; i++)
            {
                var res = await GetAsync<ResultModel2<List<TenantDTO>>>($"api/Tenants?PageNumber={i}&PageSize=100");
                if (res != null)
                {
                    result.AddRange(res.data ?? new List<TenantDTO>());
                    if (res.page.total == result.Count)
                        return result;
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
            return result;
        }
    }
}
