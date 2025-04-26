using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSP.Category.GeneralTreeManagement;
using CSP_NET.Work.Common;
using CTIN.Abp.Domain.Repositories;

namespace CSP_NET.Work.WorkManagement;

public interface ICspWorkRepository : IRepository<CspWork, Guid>
{
    Task<List<CspWork>> GetChildrenAsync(
        Guid? parentId,
        bool recursive = false,
        CancellationToken cancellationToken = default);
    Task<List<CspWork>> GetParentAsync(
        string code,
        int level,
        Guid parentId,
        bool hasFamily = false,
        bool recursive = false,
        CancellationToken cancellationToken = default);
    Task<List<CspWork>> GetListAsync(
        string sorting,
        string? keyword,
        string? workCode,
        string? taskCode,
        string? priority,
        Guid? projectId,
        Guid? parentId,
        CspWorkStatus? status,
        Guid? ownerId,
        Guid? assignerId,
        Guid? performerId,
        Guid? collaboratorId,
        Guid? currUserId,
        int? skipCount = 0,
        int? maxResultCount = int.MaxValue,
        bool onlyRootNode = true,
        bool include = false,
        bool hasChildren = false,
        CancellationToken cancellationToken = default);
    Task<IQueryable<CspWork>> CreateFilteredQueryAsync(
       string? keyword,
       string? workCode,
       string? taskCode,
       string? priority,
       Guid? projectId,
       Guid? parentId,
       CspWorkStatus? Status,
       Guid? ownerId,
       Guid? assignerId,
       Guid? performerId,
       Guid? collaboratorId,
        Guid? currUserId,
       bool onlyRootNode = true,
       bool include = false,
       bool hasChildren = false);
    Task<int> GetCountAsync(
       string? keyword,
       string? workCode,
       string? taskCode,
       string? priority,
       Guid? projectId,
       Guid? parentId,
       CspWorkStatus? Status,
        Guid? ownerId,
        Guid? assignerId,
        Guid? performerId,
        Guid? collaboratorId,
        Guid? currUserId,
       bool onlyRootNode = true);

    Task<List<CspWork>> GetListAllAsync(
      string sorting,
      string filter = null
  );
    Task<List<CspWork>> GetListAllWork(List<Guid> Ids);

}
