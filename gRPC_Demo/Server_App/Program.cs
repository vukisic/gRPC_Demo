using Grpc.Core;
using System;

namespace Server_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { ProductService.BindService(new ProductServiceImplementation()) },
                Ports = { new ServerPort("localhost", 44558, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port 44558");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
