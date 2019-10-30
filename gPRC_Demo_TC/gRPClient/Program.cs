using gPRCService;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace gRPClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest() { Name = "Vuk" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var replay = await client.SayHelloAsync(input);
            //Console.WriteLine(replay.Message);

            var request = new CustomerLookupModel() { UserId = 1 };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);
            var replay = await client.GetCustomerInfoAsync(request);
            Console.WriteLine($"{replay.FirstName}-{replay.LastName}-{replay.IsAlive}-{replay.EmailAddress}");
            
            using(var call = client.GetCustomers(new NewCustomersRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var cust = call.ResponseStream.Current;
                    Console.WriteLine($"{cust.FirstName}-{cust.LastName}-{cust.IsAlive}-{cust.EmailAddress}");
                }
            }
            
            Console.ReadLine();
        }
    }
}
