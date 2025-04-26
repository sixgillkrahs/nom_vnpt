using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSP_NET.Work.ProjectGenerals;

public static class ProjectGeneralEfCoreQueryableExtensions
{
    public static IQueryable<ProjectGeneral> IncludeDetails(this IQueryable<ProjectGeneral> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: CTIN.Csp.CliHelper generated
            ;
    }
}
