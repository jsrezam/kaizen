using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaizen.Core.Models;

namespace Kaizen.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Category> ApplyFiltering(this IQueryable<Category> query, CategoryQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.Name))
                query = query.Where(c => c.Name == queryObj.Name);

            return query;
        }

        public static IQueryable<Product> ApplyFiltering(this IQueryable<Product> query, ProductQuery queryObj, bool isExact = true)
        {
            if (queryObj.Id.HasValue)
                query = query.Where(c => c.Id == queryObj.Id);

            if (queryObj.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == queryObj.CategoryId);

            if (isExact)
            {
                if (!string.IsNullOrEmpty(queryObj.Name))
                    query = query.Where(c => c.Name.Equals(queryObj.Name));
                return query;
            }

            if (!string.IsNullOrEmpty(queryObj.Name))
                query = query.Where(c => c.Name.Contains(queryObj.Name));
            return query;
        }
        public static IQueryable<Customer> ApplyFiltering(this IQueryable<Customer> query, CustomerQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.FirstName))
                query = query.Where(c => c.FirstName == queryObj.FirstName);
            if (!string.IsNullOrEmpty(queryObj.LastName))
                query = query.Where(c => c.LastName == queryObj.LastName);
            if (!string.IsNullOrEmpty(queryObj.CellPhone))
                query = query.Where(c => c.CellPhone == queryObj.CellPhone);
            return query;
        }
        public static IQueryable<Campaign> ApplyFiltering(this IQueryable<Campaign> query, CampaignQuery queryObj)
        {
            if (queryObj.FinishDate != null)
                query = query.Where(c => c.FinishDate == queryObj.FinishDate);

            return query;
        }
        public static IQueryable<CampaignDetail> ApplyFiltering(this IQueryable<CampaignDetail> query, CampaignDetailQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.Status))
                query = query.Where(c => c.Status == queryObj.Status);

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