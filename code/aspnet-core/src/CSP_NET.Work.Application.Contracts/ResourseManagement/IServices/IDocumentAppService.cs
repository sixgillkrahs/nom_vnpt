using CSP_NET.Work.ResourseManagement.Dtos;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ResourseManagement.IServices
{
    public interface IDocumentAppService:IApplicationService
    {
        Task SaveBytesAsync(List<IFormFile> files, Guid WorkId);
        Task DeleteBytesAsync(string name);
        Task<PagedResultDto<DocumentDto>> GetAll(DocumentGetListInput input);
    }
}
