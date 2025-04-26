using CSP_NET.Work.ConfigworkAccepts;
using CTIN.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.LogWorks;

public interface ILogWorkRepository : IRepository<LogWork, Guid>
{
    Task<LogWork> FindByWorkIdAsync(Guid WorkId);
    Task<LogWork> FindByIdAsync(Guid id);
}
