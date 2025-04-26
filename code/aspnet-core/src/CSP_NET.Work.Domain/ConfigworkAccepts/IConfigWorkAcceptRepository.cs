using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTIN.Abp.Domain.Repositories;

namespace CSP_NET.Work.ConfigworkAccepts;

public interface IConfigWorkAcceptRepository : IRepository<ConfigWorkAccept, Guid>
{
    Task<ConfigWorkAccept> FindByWorkIdAsync(Guid WorkId);
    Task<ConfigWorkAccept> FindByIdAsync(Guid id);
    
}
