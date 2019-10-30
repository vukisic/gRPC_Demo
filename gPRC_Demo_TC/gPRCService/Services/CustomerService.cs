using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gPRCService.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            this.logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            return Task.FromResult(new CustomerModel()
            {
                Age = 22,
                EmailAddress = "pera@pera.com",
                FirstName = "Pera",
                LastName = "Pera",
                IsAlive = true
            });
        }

        public override async Task GetCustomers(NewCustomersRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var output = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Age = 5,
                    LastName = "Zika",
                    EmailAddress = "zika@zika.com",
                    IsAlive = true,
                    FirstName = "Zika"
                },
                new CustomerModel
                {
                    Age = 5,
                    LastName = "Zika",
                    EmailAddress = "zika@zika.com",
                    IsAlive = true,
                    FirstName = "Zika"
                },
                new CustomerModel
                {
                    Age = 5,
                    LastName = "Zika",
                    EmailAddress = "zika@zika.com",
                    IsAlive = true,
                    FirstName = "Zika"
                }

            };

            foreach (var item in output)
            {
               await responseStream.WriteAsync(item);
            }
        }
    }
}
