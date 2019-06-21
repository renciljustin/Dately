using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dately.Core.Queries;

namespace Dately.Shared.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> obj, BaseQuery query, Dictionary<string, Expression<Func<TEntity, object>>> columnsMap)
        {
            if (query.SortBy != null && columnsMap.ContainsKey(query.SortBy.ToLower()))
            {
                return !query.IsOrderDescending
                    ? obj.OrderBy(columnsMap[query.SortBy.ToLower()])
                    : obj.OrderByDescending(columnsMap[query.SortBy.ToLower()]);
            }
            return obj;
        }

        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> obj, BaseQuery query)
        {
            if (query.Page <= 0)
                query.Page = 1;

            if (query.PageSize <= 0)
                query.PageSize = 50;

            return obj.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);
        }
    }
}