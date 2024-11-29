using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> Query, Specifications<T> specifications) where T : class
        {
            var query = Query;
            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.IncludesExpression is not null)
            {
                query = specifications.IncludesExpression.Aggregate(query, (currentquery, expressions) =>
                currentquery.Include(expressions));



            }
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
               
                query = query.OrderByDescending(specifications.OrderByDescending);
            }

            return query;
        }
    }
}
