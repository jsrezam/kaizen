using System.Threading.Tasks;
using System.Linq;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Core.Interfaces;
using Kaizen.Core.DTOs;
using System;
using System.Collections.Generic;

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
            var campaignDetail = await unitOfWork.CampaignDetailRepository.GetCampaignDetailAsync(campaignId, new CampaignDetailQuery { ApplyPagingFromClient = true });


            var query = (from c in customers.Items
                         join cd in campaignDetail.Items on c.Id equals cd.CustomerId into temp
                         from cd in temp.DefaultIfEmpty()
                         where cd == null
                         select new Customer
                         {
                             Id = c.Id,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             Email = c.Email,
                             Address = c.Address,
                             CellPhone = c.CellPhone,
                             City = c.City,
                             Region = c.Region,
                             Country = c.Country,
                             HomePhone = c.HomePhone,
                             IdentificationCard = c.IdentificationCard,
                             PostalCode = c.PostalCode,
                             State = c.State
                         }).AsQueryable();

            result.TotalItems = query.Count();
            result.Items = query.ToList();

            return result;
        }

        public async Task<QueryResult<AgentCustomer>> GetAgentCustomersAsync(string agentId, CustomerQuery customerQuery)
        {
            var result = new QueryResult<AgentCustomer>();

            var userCampaigns = await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(agentId, new CampaignQuery());

            var query = (from userCampaign in userCampaigns.Items
                         from CampaignDetail in userCampaign.CampaignDetails
                         select new AgentCustomer
                         {
                             Customer = CampaignDetail.Customer,
                             CampaignDetailId = CampaignDetail.Id,
                             CampaignId = CampaignDetail.CampaignId
                         }).AsQueryable();

            if (customerQuery != null)
            {
                if (!string.IsNullOrEmpty(customerQuery.FirstName))
                    query = query.Where(c => c.Customer.FirstName.ToUpper().Trim().Equals(customerQuery.FirstName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.LastName))
                    query = query.Where(c => c.Customer.LastName.ToUpper().Trim().Equals(customerQuery.LastName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.CellPhone))
                    query = query.Where(c => c.Customer.CellPhone.ToUpper().Trim().Equals(customerQuery.CellPhone.ToUpper().Trim()));
            }

            result.TotalItems = query.Count();
            result.Items = query.ToList();
            return result;
        }

        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            return await unitOfWork.CustomerRepository.isUniqueCellphone(cellPhone);
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

        public async Task<QueryResult<Customer>> GetRandomCustomersAsync(int maxRange)
        {
            var result = new QueryResult<Customer>();
            var customersIds = new List<int>();
            var randomCustomers = new List<Customer>();
            int count = 0;

            var customers = await GetCustomersAsync(new CustomerQuery { ApplyPagingFromClient = true });

            if (maxRange >= customers.TotalItems)
                maxRange = customers.TotalItems;

            customersIds.AddRange(
                from customer in customers.Items
                select customer.Id
            );

            while (count < maxRange)
            {
                var randomNumber = (new Random()).Next(customersIds.Count);
                var randomCustomer = customers.Items.FirstOrDefault(c => c.Id == customersIds[randomNumber]);

                if (randomCustomers.FindIndex(cr => cr.Id == randomCustomer.Id) == -1)
                {
                    randomCustomers.Add(randomCustomer);
                    customersIds.RemoveAt(randomNumber);
                    count++;
                }
            }

            result.TotalItems = randomCustomers.Count();
            result.Items = randomCustomers;

            return result;
        }

    }
}