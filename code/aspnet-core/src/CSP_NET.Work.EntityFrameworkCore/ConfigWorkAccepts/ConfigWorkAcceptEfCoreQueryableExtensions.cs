using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSP_NET.Work.ConfigworkAccepts;

public static class ConfigWorkAcceptEfCoreQueryableExtensions
{
    public static IQueryable<ConfigWorkAccept> IncludeDetails(this IQueryable<ConfigWorkAccept> queryable, bool include = true)
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
