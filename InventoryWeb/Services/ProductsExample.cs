using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using ServiceStack;

namespace InventoryWeb.Services
{
    [Api("Product Service Description")]
    [Route("/product/{EAN}", "GET", Summary = @"GET Summary", Notes = "GET Notes")]
    public class GetProduct : IReturn<Product>
    {
        [ApiMember(Name = "EAN", Description = "Product EAN", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string EAN { get; set; }
    }

    [Api("Product Service Description")]
    [Route("/product", "GET", Summary = @"GET Summary", Notes = "GET Notes")]
    //[Route("/product/{TPNB*}", "GET", Summary = @"GET Summary", Notes = "GET Notes")]
    //[Route("/product/{Category}", "GET", Summary = @"Get all products matching the category", Notes = "GET Notes")]
    public class FindProducts : IReturn<List<Product>>
    {
        [ApiMember(Name = "TPNB", Description = "Tesco Identifier", ParameterType = "query", DataType = "string", IsRequired = false)]
        public string TPNB { get; set; }
        [ApiMember(Name = "Category", Description = "Tesco Category", ParameterType = "query", DataType = "string", IsRequired = false)]
        public string Category { get; set; }
    }

    public class ProductService : IService
    {
        readonly Product[] products = new[] 
        { 
            new Product { Id = 1, Category = "H71ZN", Description ="PAN BRA TANGO BALCONETTEPINK 34GG" , EAN = "5051928340377" , PureIdentity = "5051928.034037", TPNB = "73552502"},
            new Product { Id = 1, Category = "H71ZN", Description ="PAN BRA TANGO BALCONETTEPINK 34H" , EAN = "5051928340384" , PureIdentity = "5051928.034038", TPNB = "73552519"},
            new Product { Id = 1, Category = "H71ZN", Description ="PAN BRA TANGO BALCONETTEPINK 34DD" , EAN = "5051928340445" , PureIdentity = "5051928.034044", TPNB = "73552525"},
            new Product { Id = 1, Category = "H71ZN", Description ="PAN BRA TANGO BALCONETTEPINK 34E" , EAN = "5051928340452" , PureIdentity = "5051928.034045", TPNB = "73552531"},
            new Product { Id = 1, Category = "H71ZN", Description ="PAN BRA TANGO BALCONETTEPINK 34F" , EAN = "5051928340469" , PureIdentity = "5051928.034046", TPNB = "73552548"},
        };

        public object Get(GetProduct request)
        {
            var product = products.FirstOrDefault(x => x.EAN == request.EAN);

            if (product == null)
                throw new HttpError(HttpStatusCode.NotFound, "Product does not exist");

            return product;

        }

        public object Get(FindProducts request)
        {
            var ret = products.AsQueryable();
            if (request.Category != null)
                ret = ret.Where(x => x.Category == request.Category);
            if (!string.IsNullOrEmpty(request.TPNB))
                ret = ret.Where(x => x.TPNB == request.TPNB);
            return ret;
        }
    }
}
