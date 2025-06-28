﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluater<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specs)
        {
            var query = inputQuery;
            if (specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);
            }
            if (specs.OrderBy != null)
            {
                query = query.OrderBy(specs.OrderBy);
            }
            if (specs.OrderByDescending != null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);
            }
            if (specs.IsPaginatd)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }

            query = specs.Includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query;
        }
    }
}
