using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSP_NET.Work.DepartmentTeams;

public static class DepartmentTeamEfCoreQueryableExtensions
{
    public static IQueryable<DepartmentTeam> IncludeDetails(this IQueryable<DepartmentTeam> queryable, bool include = true)
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
