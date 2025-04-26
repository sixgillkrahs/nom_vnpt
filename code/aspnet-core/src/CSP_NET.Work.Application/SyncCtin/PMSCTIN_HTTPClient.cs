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
    public interface IPMSCTIN_HTTPClient
    {
        Task<CTINProject> GetProject(string code);
    }
    public class PMSCTIN_HTTPClient : BaseHttpClient, IPMSCTIN_HTTPClient, IScopedDependency
    {
        public PMSCTIN_HTTPClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory, SyncCtinConsts.PMS)
        {

        }

        public async Task<CTINProject> GetProject(string code)
        {
            var result = await GetAsync<ResultModel<List<CTINProject>>>($"api/ProjGeneral?totalPage=1&count=3&where=%7B%22and%22%3A%20%5B%7B%22projCode%22%3A%20%22{code}%22%7D%5D%7D");
            return result?.data.Count > 0 ? result?.data[0] : null;
        }
    }

    public class CTINProject
    {
        public Guid id {  get; set; }
        public string projCode {  get; set; }
        public string projName { get; set; }
        public int departmentId { get; set; }
        public int projPhase { get; set; }
    }
    public class RequestProjectContext
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Status { get; set; }

    }
    public class ResponseProjectContext
    {
        public int TotalRow { get; set; }
        public List<CTINProject> Items { get; set; }

    }
}
