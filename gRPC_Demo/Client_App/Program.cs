using Grpc.Core;
using System;

namespace Client_App
{
    class Program
    {
        static ProductService.ProductServiceClient client;
        static void Main(string[] args)
        {
            Channel channel = new Channel("localhost:44558", ChannelCredentials.Insecure);
            client = new ProductService.ProductServiceClient(channel);
            int op = 0;
            do
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("1. Get");
                Console.WriteLine("2. GetAll");
                Console.WriteLine("3. Add");
                Console.WriteLine("4. Remove");
                Console.WriteLine("0. Exit");
                Console.Write(">>");
                bool res = Int32.TryParse(Console.ReadLine(), out op);
                if (!res)
                    continue;

                switch (op)
                {
                    case 1: Get();  break;
                    case 2: GetAll(); break;
                    case 3: Add(); break;
                    case 4: Remove(); break;
                    case 0: break;
                    default: Console.WriteLine("Error!"); break;
                }

            } while (op != 0);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void GetAll()
        {
            var replay = client.GetAll(new None());

            Console.WriteLine("\nAll");
            foreach (var item in replay.List)
            {
                Console.WriteLine($"{item.Name}");
            }
        }

        public static void Get()
        {
            Console.Write("Name:");
            string name = Console.ReadLine();

            var replay = client.Get(new ProductRequest() { Name = name });
            if(replay == null)
                Console.WriteLine("No Product!");
            else
                Console.WriteLine($"{replay.Name}");
        }

        public static void Add()
        {
            Console.Write("Name:");
            string name = Console.ReadLine();

            var replay = client.Add(new Product() { Name = name });
            if (replay.Success)
                Console.WriteLine("Success!");
            else
                Console.WriteLine("Already Exist!");
        }

        public static void Remove()
        {
            Console.Write("Name:");
            string name = Console.ReadLine();

            var replay = client.Remove(new ProductRequest { Name = name });
            if (replay.Success)
                Console.WriteLine("Success!");
            else
                Console.WriteLine("Already Exist!");
        }


    }
}
