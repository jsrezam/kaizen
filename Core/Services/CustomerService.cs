using System.Threading.Tasks;
using System.Linq;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Core.Interfaces;
using Kaizen.Core.DTOs;
using System;
using System.Collections.Generic;
using Kaizen.Infrastructure.Extensions;
using System.Linq.Expressions;
using Kaizen.Controllers.Enumerations;

namespace Kaizen.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILocationService locationService;
        public CustomerService(IUnitOfWork unitOfWork, ILocationService locationService)
        {
            this.locationService = locationService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj)
        {
            return await unitOfWork.CustomerRepository.GetCustomersAsync(queryObj);
        }
        public async Task<QueryResult<Customer>> GetNoInCampaignCustomersAsync(int campaignId, CustomerQuery queryObj)
        {
            var result = new QueryResult<Customer>();

            var customers = await unitOfWork.CustomerRepository.GetCustomersAsync(queryObj);
            var campaignDetail = (await unitOfWork
            .CampaignDetailRepository
            .GetCampaignDetailAsync(campaignId, new CampaignDetailQuery { ApplyPagingFromClient = true }))
            .Items.Where(cd => !cd.State.Equals(CampaignStatus.Earned.ToString()));


            var query = (from c in customers.Items
                         join cd in campaignDetail on c.Id equals cd.CustomerId into temp
                         from cd in temp.DefaultIfEmpty()
                         where cd == null
                         select c).AsQueryable();

            result.TotalItems = query.Count();
            result.Items = query.ToList();

            return result;
        }

        public async Task<QueryResult<AgentCustomerViewModel>> GetAgentCustomersAsync(string agentId, CustomerQuery queryObj)
        {
            var campaigns = await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(agentId, new CampaignQuery());

            return unitOfWork.CustomerRepository.GetAgentCustomers(campaigns, queryObj);
        }

        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            var customerResponse = await unitOfWork.CustomerRepository.isUniqueCellphone(cellPhone);
            var userResponse = await unitOfWork.UserRepository.isUniqueCellphone(cellPhone);

            return customerResponse && userResponse;
        }

        public async Task CreateCustomer(Customer customer)
        {
            await unitOfWork.CustomerRepository.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            return await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        }

        public async Task<CustomerDto> GetLocationNames(CustomerDto customerDto)
        {
            var location = await locationService.GetLocationNames(customerDto.CountryId, customerDto.RegionId, customerDto.CityId);
            customerDto.Country = location.Country;
            customerDto.Region = location.Region;
            customerDto.City = location.City;
            return customerDto;
        }

        public async Task<CustomerDto> GetLocationIds(CustomerDto customerDto)
        {
            var location = await locationService.GetLocationIds(customerDto.Country, customerDto.Region, customerDto.City);
            customerDto.CountryId = location.CountryId;
            customerDto.RegionId = location.RegionId;
            customerDto.CityId = location.CityId;
            return customerDto;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            unitOfWork.CustomerRepository.Update(customer);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<QueryResult<Customer>> GetRandomCustomersAsync(int maxRange, ApplicationUserQueryDto querObj)
        {
            var result = new QueryResult<Customer>();

            var query = (await GetAgentAvailableCustomersAsync(querObj.Id)).Items
            .OrderBy(r => (new Random()).Next())
            .Take(maxRange);

            result.TotalItems = query.Count();
            result.Items = query;

            return result;
        }

        public async Task<QueryResult<Customer>> GetAgentAvailableCustomersAsync(string agentId)
        {
            var result = new QueryResult<Customer>();

            var customersInProgressCampaign = await unitOfWork.CustomerRepository.GetCustomersInProgressCampaign(agentId);

            var query = (
                            from cus in await unitOfWork.CustomerRepository.GetAllNoTracking()
                            join cipc in customersInProgressCampaign.Items
                                on cus.Id equals cipc.Id into temp
                            from cipc in temp.DefaultIfEmpty()
                            where cipc == null && cus.State
                            select cus
                        );

            result.TotalItems = query.Count();
            result.Items = query.ToList();

            return result;
        }
    }
}