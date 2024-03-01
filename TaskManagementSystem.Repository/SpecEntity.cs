using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Spacifications;

namespace TaskManagementSystem.Repository
{
    internal static class SpecEntity<Tentity> where Tentity : BaseClass
    {
        public static IQueryable<Tentity> Query(IQueryable<Tentity> inputQuery, ISpacification<Tentity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.TaskatBy is not null)
            {
                query = query.OrderBy(spec.TaskatBy);
            }
          
            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (Current, second) => Current.Include(second));

            return query;
        }
    }
}
