
using CSP_NET.Work.LogWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class LogWorkEfCoreQueryableExtensions
{
    public static IQueryable<LogWork> IncludeDetails(this IQueryable<LogWork> queryable, bool include = true)
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

