using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace Server_App
{
    
    public class ProductServiceImplementation : ProductService.ProductServiceBase
    {
        public static List<Product> Products { get; set; } = new List<Product>();
        public override Task<BoolMessage> Add(Product request, ServerCallContext context)
        {
            var result = Products.SingleOrDefault(x => x.Name == request.Name);
            bool success = false;
            if(result == null)
            {
                Products.Add(new Product() { Name = request.Name });
                success = true;
            }
            return Task.FromResult(new BoolMessage() { Success = success });
        }

        public override Task<Product> Get(ProductRequest request, ServerCallContext context)
        {
            var result = Products.SingleOrDefault(x => x.Name == request.Name);
            if (result != null)
            {
                return Task.FromResult<Product>(result);
            }
            return Task.FromResult<Product>(new Product() {Name = "No Poduct!" });

        }

        public override Task<AllProducts> GetAll(None request, ServerCallContext context)
        {
            AllProducts all = new AllProducts();
            all.List.AddRange(Products);
            return Task.FromResult(all);
        }

        public override Task<BoolMessage> Remove(ProductRequest request, ServerCallContext context)
        {
            var result = Products.SingleOrDefault(x => x.Name == request.Name);
            bool success = false;
            if (result != null)
            {
                int index = Products.IndexOf(result);
                Products.RemoveAt(index);
                success = true;
            }
            return Task.FromResult(new BoolMessage() { Success = success });
        }
    }
}
