using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Domain.Entities;
using CTIN.Abp.Domain.Repositories.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ResourseManagement
{
    internal class DocumentRepository : EfCoreRepository<WorkDbContext, Documents, Guid>,IDocumentRepository
    {
        public DocumentRepository(IDbContextProvider<WorkDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task DeleteAsync(Guid id)
        {
            var dbset = await GetDbSetAsync();
            var doc = await dbset.FindAsync(id);
            dbset.Remove(doc);
        }

        public async Task<Guid> getIdByName(string name)
        {
            var dbset = await GetDbSetAsync();
            var document = await dbset.FirstOrDefaultAsync(x => x.Name == name);
            if (document == null)
            {
                throw new Exception("Không tìm thấy đối tượng với tên cụ thể");
            }
           
            return document.Id;
        }

        public async Task insert(Documents document)
        {
            var dbset = await GetDbSetAsync();
            await dbset.AddAsync(document);
            
        }
    }
}
