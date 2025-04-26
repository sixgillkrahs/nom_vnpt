using CSP_NET.Work.SyncCtin.DTO;
using CTIN.Abp.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin
{
    public interface IIDSUAT_HTTPClient
    {
        Task<List<UATUser>> GetUsers();
    }

    public class IDSUAT_HTTPClient : BaseHttpClient, IIDSUAT_HTTPClient, IScopedDependency
    {
        public IDSUAT_HTTPClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory, SyncCtinConsts.IDSUAT)
        {
        }

        public async Task<List<UATUser>> GetUsers()
        {
            var result = new List<UATUser>();
            for (var i = 1; i <= 10000; i++)
            {
                var res = await GetAsync<ResultModelUAT<List<UATUser>>>($"api/Employees?PageNumber={i}&PageSize=100&where=%7B%22dataDb.status%22:%7B%22neq%22:3%7D%7D");
                result.AddRange(res?.data ?? new List<UATUser>());
                if (res.page.total == result.Count)
                    return result;
                Task.Delay(TimeSpan.FromSeconds(10));
            }
            return result;
        }
    }

    public class UATUser
    {
        public long employeeId { get; set; }

        public string employeeCode { get; set; }

        public string userName { get; set; }

        //public string[] roles { get; set; }
    }
    public class ResultModelUAT<T>
    {
        public SerializableError error { get; set; }

        public T data { get; set; }

        public PagingModelUAT page { get; set; }
    }
    public class PagingModelUAT
    {

        public int? total { get; set; }

        public int number { get; set; }

        public int size { get; set; }

    }
}
