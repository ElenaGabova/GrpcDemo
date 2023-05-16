// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System.Text;

var channel = GrpcChannel.ForAddress("https://localhost:7236/");
var result = await CustomersServiceGetNewCustomersTesting();
Console.WriteLine(result);
Console.ReadLine();

async Task<string> CustomersServiceGetCustomerInfoAsyncTesting()
{
    var input = new CustomerLookupModel {UserId = 1};
    var client = new Customer.CustomerClient(channel);
    var customer = await client.GetCustomerInfoAsync(input);
    return customer.FirstName + " " + customer.LastName;
}

async Task<string> CustomersServiceGetNewCustomersTesting()
{
    var client = new Customer.CustomerClient(channel);
    StringBuilder output = new StringBuilder();
    using(var call = client.GetNewCustomers(new NewCustomerReqest()))
    {
        while(await call.ResponseStream.MoveNext())
        {
            var currentCustomer = call.ResponseStream.Current;
            output.AppendLine(currentCustomer.FirstName + " " + currentCustomer.LastName + " :" + currentCustomer.EmailAdress);
        }
    }

    return output.ToString();
}


async Task<string> GreeterServiceSayHelloAsyncTesting()
{
    var input  = new HelloRequest{ Name = "GrpcName" };
    var client = new Greeter.GreeterClient(channel);
    var reply = await client.SayHelloAsync(input);
    return reply.Message;
}
