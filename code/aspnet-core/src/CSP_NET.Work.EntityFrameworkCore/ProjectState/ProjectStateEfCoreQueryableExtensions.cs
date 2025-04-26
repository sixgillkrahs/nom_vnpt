using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSP_NET.Work.ProjectState;

public static class ProjectStateEfCoreQueryableExtensions
{
    public static IQueryable<ProjectState> IncludeDetails(this IQueryable<ProjectState> queryable, bool include = true)
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
