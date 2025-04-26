using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.DependencyInjection;
using CTIN.Abp.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin
{
    public interface IHRMCTIN_HTTPClient
    {
        Task<List<CompanyInfo>> GetDepartent();

        Task<List<CTINEmployee>> GetEmployees();
    }
    public class HRMCTIN_HTTPClient : BaseHttpClient, IHRMCTIN_HTTPClient, IScopedDependency
    {
        public HRMCTIN_HTTPClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory, SyncCtinConsts.HRM)
        {

        }

        public async Task<List<CompanyInfo>> GetDepartent()
        {
            var result = await GetAsync<ResultModel<List<CompanyInfo>>>("api/company?size=1000&where=%7B%22dataDb.status%22:%7B%22neq%22:3%7D%7D");
           
            return result?.data ?? new List<CompanyInfo>();
        }

        public async Task<List<CTINEmployee>> GetEmployees()
        {
            //var result = await GetAsync<ResultModel<List<CTINEmployee>>>("api/employees?page=1?size=100&where=%7B%22dataDb.status%22:%7B%22neq%22:3%7D%7D");
            //return result?.data ?? new List<CTINEmployee>();

            var result = new List<CTINEmployee>();
            for (var i = 1; i <= 10000; i++)
            {
                var res = await GetAsync<ResultModel<List<CTINEmployee>>>($"api/employees?page={i}&size=100&where=%7B%22dataDb.status%22:%7B%22neq%22:3%7D%7D");
                result.AddRange(res?.data ?? new List<CTINEmployee>());
                if (res?.paging.count == result.Count)
                    return result;
            }
            return result;
        }
    }
}
