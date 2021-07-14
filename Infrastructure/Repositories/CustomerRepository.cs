using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Infrastructure.Extensions;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(KaizenDbContext context) : base(context) { }

        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            var customer = await entities
            .SingleOrDefaultAsync(c => c.CellPhone.Equals(cellPhone));
            return customer == null;
        }

        public async Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj)
        {
            var result = new QueryResult<Customer>();
            var query = entities.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Customer, object>>>()
            {
                ["firstName"] = c => c.FirstName,
                ["lastName"] = c => c.LastName,
                ["identificationCard"] = c => c.IdentificationCard,
                ["email"] = c => c.Email,
                ["cellPhone"] = c => c.CellPhone,
                ["city"] = c => c.City,
                ["region"] = c => c.Region,
                ["country"] = c => c.Country,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Customer>> GetCustomersInProgressCampaign(string agentId)
        {
            var result = new QueryResult<Customer>();
            var query = entities
            .AsNoTracking()
            .Where(c => c.CampaignDetails
                .Any(cd => cd.Campaign.UserId.Equals(agentId)
                    && cd.Campaign.IsActive
                    && !cd.State.Equals(CampaignStatus.Earned.ToString())))
                    .Select(c => new Customer
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Address = c.Address,
                        CellPhone = c.CellPhone,
                        City = c.City,
                        Country = c.Country,
                        Email = c.Email,
                        HomePhone = c.HomePhone,
                        IdentificationCard = c.IdentificationCard,
                        PostalCode = c.PostalCode,
                        Region = c.Region,
                        State = c.State
                    })
            .AsQueryable();

            result.TotalItems = await query.CountAsync();
            result.Items = await query.ToListAsync();

            return result;
        }

        public QueryResult<AgentCustomerViewModel> GetAgentCustomers(QueryResult<Campaign> campaigns, CustomerQuery queryObj)
        {
            var result = new QueryResult<AgentCustomerViewModel>();

            var query = (from campaign in campaigns.Items
                         from CampaignDetail in campaign.CampaignDetails
                         select new AgentCustomerViewModel
                         {
                             Customer = CampaignDetail.Customer,
                             CampaignDetailId = CampaignDetail.Id,
                             CampaignId = campaign.Id
                         }).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<AgentCustomerViewModel, object>>>()
            {
                ["customer.firstName"] = c => c.Customer.FirstName,
                ["customer.lastName"] = c => c.Customer.LastName,
                ["customer.identificationCard"] = c => c.Customer.IdentificationCard,
                ["customer.email"] = c => c.Customer.Email,
                ["customer.cellPhone"] = c => c.Customer.CellPhone,
                ["customer.city"] = c => c.Customer.City,
                ["customer.region"] = c => c.Customer.Region,
                ["customer.country"] = c => c.Customer.Country,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = query.Count();
            query = query.ApplyPaging(queryObj);
            result.Items = query.ToList();

            return result;
        }
    }
}