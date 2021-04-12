using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaizen.Core.Models;

namespace Kaizen.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Category> ApplyFiltering(this IQueryable<Category> query, CategoryQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.Name))
                query = query.Where(c => c.Name == queryObj.Name);

            return query;
        }

        public static IQueryable<Product> ApplyFiltering(this IQueryable<Product> query, ProductQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.Name))
                query = query.Where(c => c.Name == queryObj.Name);

            return query;
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            return query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}