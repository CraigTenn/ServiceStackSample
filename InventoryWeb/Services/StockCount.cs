using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWeb.Services
{
    public class StockCount
    {
        public int StockCountId { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }

    public class Location
    {
        public int LocationId { get; set; }

        public string Name { get; set; }
    }

    public class ProductCategory
    {
        public int CategoryId { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }
    }

    public class ProductHierarchy
    {
        public List<ProductCategory> ProductCategories { get; set; }
    }
}