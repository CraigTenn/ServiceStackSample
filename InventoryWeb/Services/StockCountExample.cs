using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWeb.Services
{
    using System.Net;

    using ServiceStack;

    [Api("Get StockCount")]
    //[Route("/stockcount/", "GET", Summary = @"GET Summary", Notes = "GET Notes")]
    [Route("/stockcount/{StockCountId}", "GET", Summary = @"GET Summary", Notes = "GET Notes")]
    public class GetStockCount : IReturn<StockCount>
    {
        [ApiMember(Name = "StockCountId", Description = "Get a stock count by ID", ParameterType = "path", DataType = "string", IsRequired = false)]
        public int StockCountId { get; set; }
    }

    [Api("Start StockCount")]
    [Route("/stockcount/", "POST", Summary = @"Start a stock count with specified data", Notes = "POST Notes")]
    public class StartStockCount : IReturn
    {
        [ApiMember(Name = "LocationId", Description = "Location for this stock count", ParameterType = "query", DataType = "int", IsRequired = true)]
        public int LocationId { get; set; }

        [ApiMember(Name = "ProductCategoryCode", Description = "Product Category for this stock count", ParameterType = "query", DataType = "string", IsRequired = true)]
        public string ProductCategoryCode { get; set; }
    }

    public class StockCountService : IService
    {
        private Location[] locations = new Location[]
        {
            new Location { LocationId = 1, Name = "Baldock" },
            new Location { LocationId = 2, Name = "Stevenage" },
        };

        private ProductCategory[] productCategories = new ProductCategory[]
        {
            new ProductCategory { CategoryId = 0, CategoryCode = "H7", CategoryName = "Clothing" }, 
            new ProductCategory { CategoryId = 1, CategoryCode = "H71", CategoryName = "Womens" }, 
            new ProductCategory { CategoryId = 2, CategoryCode = "H72", CategoryName = "Toddlers" }, 
            new ProductCategory { CategoryId = 3, CategoryCode = "H73", CategoryName = "Baby" }, 
            new ProductCategory { CategoryId = 4, CategoryCode = "H74", CategoryName = "Girls" }, 
            new ProductCategory { CategoryId = 5, CategoryCode = "H75", CategoryName = "Boys" }, 
            new ProductCategory { CategoryId = 6, CategoryCode = "H76", CategoryName = "Mens" }, 
            new ProductCategory { CategoryId = 7, CategoryCode = "H77", CategoryName = "Schoolwear" }, 
            new ProductCategory { CategoryId = 8, CategoryCode = "H78", CategoryName = "Footwear" }, 
            new ProductCategory { CategoryId = 9, CategoryCode = "H79", CategoryName = "Underwear" }
        };

        private List<StockCount> inProgressStockCounts = new List<StockCount>
        {
            new StockCount{ StockCountId = 1, Description = "Baldock - Clothing" },
            new StockCount{ StockCountId = 2, Description = "Baldock - Menswear" },
            new StockCount{ StockCountId = 3, Description = "Stevanage - Clothing" },
            new StockCount{ StockCountId = 4, Description = "Stevanage - Kidswear" }
        };

        public StockCount Get(GetStockCount request)
        {
            var stockCount = inProgressStockCounts
                .FirstOrDefault(x => x.StockCountId == request.StockCountId);

            if (stockCount == null)
                throw new HttpError(HttpStatusCode.NotFound, string.Format("No stock count found with id {0}", request.StockCountId));

            return stockCount;
        }

        public object Post(StartStockCount request)
        {
            var maxId = inProgressStockCounts.Max(x => x.StockCountId);
            var location = locations.FirstOrDefault(x => x.LocationId == request.LocationId);
            var category = productCategories.FirstOrDefault(x => x.CategoryCode == request.ProductCategoryCode);
            if (location == null || category == null)
            {
                //TODO: What happens when Tesco services throw exceptions?
                throw new HttpError(HttpStatusCode.NotAcceptable, string.Format("Unacceptable location or product code"));
            }
            else
            {
                var newStockCount = new StockCount
                                              {
                                                  StockCountId = maxId + 1,
                                                  Location = location,
                                                  ProductCategory = category,
                                                  Description = string.Format("{0} - {1}", location.Name, category.CategoryName)
                                              };
                inProgressStockCounts.Add(newStockCount);
                return new HttpResult(newStockCount.StockCountId, HttpStatusCode.Accepted);
            }
        }
    }
}
