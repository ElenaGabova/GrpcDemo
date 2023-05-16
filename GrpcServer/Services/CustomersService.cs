using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }
        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            switch (request.UserId)
            {
                case 1:
                    {
                        output.FirstName = "Jammie";
                        output.LastName = "Smith";
                        break;
                    }
                case 2:
                    {
                        output.FirstName = "Sam";
                        output.LastName = "Smith";
                        break;
                    }
                case 3:
                    {
                        output.FirstName = "Ann";
                        output.LastName = "Smith";
                        break;
                    }

                default:
                    {
                        output.FirstName = "Lena";
                        output.LastName = "Smith"; 
                        break;
                    }
            }

            return Task.FromResult(output);

        }
        public override async Task GetNewCustomers(NewCustomerReqest request, 
                                                   IServerStreamWriter<CustomerModel> responseStream, 
                                                   ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Jammie",
                    LastName = "Smith",
                    EmailAdress ="jammie@google.com",
                    Age = 30,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Sam",
                    LastName = "Smith",
                    EmailAdress ="sam@google.com",
                    Age = 25,
                    IsAlive = false
                },
                new CustomerModel
                {
                    FirstName = "ann",
                    LastName = "Smith",
                    EmailAdress ="ann@google.com",
                    Age = 35,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
                await responseStream.WriteAsync(customer);
        }
    }
}
