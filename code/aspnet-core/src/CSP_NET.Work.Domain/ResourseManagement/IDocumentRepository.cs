using CTIN.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ResourseManagement
{
    public interface IDocumentRepository:IRepository<Documents,Guid>
    {
        Task<Guid> getIdByName(string name);
        Task insert(Documents document);
        Task DeleteAsync(Guid id);
    }
}
