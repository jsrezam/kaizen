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
                query = query.Where(c => c.Name.Contains(queryObj.Name));

            return query;
        }

        public static IQueryable<Product> ApplyFiltering(this IQueryable<Product> query, ProductQuery queryObj)
        {
            if (queryObj.Id.HasValue)
                query = query.Where(c => c.Id == queryObj.Id);

            if (queryObj.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == queryObj.CategoryId);

            if (!string.IsNullOrEmpty(queryObj.Name))
                query = query.Where(c => c.Name.Contains(queryObj.Name));

            return query;
        }

        public static IQueryable<Customer> ApplyFiltering(this IQueryable<Customer> query, CustomerQuery queryObj)
        {
            if (!string.IsNullOrEmpty(queryObj.FirstName))
                query = query.Where(c => c.FirstName.Contains(queryObj.FirstName.Trim()));
            if (!string.IsNullOrEmpty(queryObj.LastName))
                query = query.Where(c => c.LastName.Contains(queryObj.LastName.Trim()));
            if (!string.IsNullOrEmpty(queryObj.CellPhone))
                query = query.Where(c => c.CellPhone.Contains(queryObj.CellPhone.Trim()));
            if (!string.IsNullOrEmpty(queryObj.IdentificationCard))
                query = query.Where(c => c.IdentificationCard.Contains(queryObj.IdentificationCard.Trim()));
            if (!string.IsNullOrEmpty(queryObj.Email))
                query = query.Where(c => c.Email.Contains(queryObj.Email.Trim()));
            if (!string.IsNullOrEmpty(queryObj.City))
                query = query.Where(c => c.City.Contains(queryObj.City.Trim()));
            if (!string.IsNullOrEmpty(queryObj.Region))
                query = query.Where(c => c.Region.Contains(queryObj.Region.Trim()));
            if (!string.IsNullOrEmpty(queryObj.Country))
                query = query.Where(c => c.Country.Contains(queryObj.Country.Trim()));

            return query;
        }

        public static IQueryable<Campaign> ApplyFiltering(this IQueryable<Campaign> query, CampaignQuery queryObj)
        {
            if (queryObj.Id.HasValue)
                query = query.Where(c => c.Id == queryObj.Id);

            return query;
        }

        public static IQueryable<CampaignDetail> ApplyFiltering(this IQueryable<CampaignDetail> query, CampaignDetailQuery queryObj)
        {
            if (queryObj.Customer != null && !string.IsNullOrEmpty(queryObj.Customer.FirstName))
                query = query.Where(c => c.Customer.FirstName.Contains(queryObj.Customer.FirstName.Trim()));

            if (queryObj.Customer != null && !string.IsNullOrEmpty(queryObj.Customer.LastName))
                query = query.Where(c => c.Customer.LastName.Contains(queryObj.Customer.LastName.Trim()));

            if (queryObj.Customer != null && !string.IsNullOrEmpty(queryObj.Customer.CellPhone))
                query = query.Where(c => c.Customer.CellPhone.Contains(queryObj.Customer.CellPhone.Trim()));

            if (queryObj.TotalCallsNumber.HasValue)
                query = query.Where(c => c.TotalCallsNumber == queryObj.TotalCallsNumber);

            if (!string.IsNullOrEmpty(queryObj.LastCallDuration))
                query = query.Where(c => c.LastCallDuration.Contains(queryObj.LastCallDuration.Trim()));

            if (!string.IsNullOrEmpty(queryObj.State))
                query = query.Where(c => c.State.Contains(queryObj.State.Trim()));

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
            if (queryObj.ApplyPagingFromClient)
                return query;

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            return query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}