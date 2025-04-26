using CSP_NET.Work.ResourseManagement.Dtos;
using CSP_NET.Work.ResourseManagement.IServices;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CTIN.Abp.BlobStoring;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.ObjectMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ResourseManagement
{
    public class DocumentAppService:ApplicationService,IDocumentAppService
    {
        private readonly IRepository<Documents, Guid> _docrepository;
        private readonly IBlobContainer<DocumentContainer> _blobContainer;
        private readonly IDocumentRepository _documentRepository;

        public DocumentAppService(IRepository<Documents, Guid> docrepository, IBlobContainer<DocumentContainer> blobContainer, IDocumentRepository documentRepository)
        {
            _docrepository = docrepository;
            _blobContainer = blobContainer;
            _documentRepository = documentRepository;
        }



        public async Task SaveBytesAsync([FromForm] List<IFormFile> files, Guid WorkId)
        {
            Console.WriteLine(files);
            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);
                var id = Guid.NewGuid();
                var newFile = new Documents(id, file.FileName, WorkId, DateTime.Now, "hello", file.Length, file.ContentType);
                var created = await _docrepository.InsertAsync(newFile);
                ObjectMapper.Map<Documents, DocumentDto>(newFile);
                await _blobContainer.SaveAsync(file.FileName, memoryStream.ToArray()).ConfigureAwait(false);
            }
        }
        public async Task<FileResult> DownloadImageAsync(string imageName,string mimeType)
        {
            byte[] imageBytes = await _blobContainer.GetAllBytesOrNullAsync(imageName);
            if (imageBytes == null)
            {
                return null;
            }
            MemoryStream memoryStream = new MemoryStream(imageBytes);
            return new FileStreamResult(memoryStream, mimeType)
            {
                FileDownloadName = imageName
            };
        }

        public async Task DeleteBytesAsync(string name)
        {
            bool imageBytes = await _blobContainer.ExistsAsync(name);
            if (imageBytes == false)
            {
                return;
            }

            Guid id =  await _documentRepository.getIdByName(name);
            Console.WriteLine(id);
            await _docrepository.DeleteAsync(id);
            await _blobContainer.DeleteAsync(name);
        }

        public async Task<PagedResultDto<DocumentDto>> GetAll(DocumentGetListInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Documents.Name);
            }
            var document = await _docrepository.GetListAsync();
            var count = document.Count();
            return new PagedResultDto<DocumentDto> { 
                TotalCount = count,
                Items = ObjectMapper.Map<List<Documents>,List<DocumentDto>>(document)
            };
           
        }
    }
}
