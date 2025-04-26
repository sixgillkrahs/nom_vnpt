using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSP_NET.Work.WorkManagement;

public static class CspWorkEfCoreQueryableExtensions
{
    public static IQueryable<CspWork> IncludeDetails(
        this IQueryable<CspWork> queryable,
        bool include = true,
        bool hasChildren = false)
    {
        if (!include)
        {
            return queryable.AsNoTracking();
        }

        return queryable
            .IncludeIf(hasChildren, x => x.Children)
            .Include(x => x.ProjectGeneral);
    }
}
