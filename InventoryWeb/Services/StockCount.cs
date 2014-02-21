using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWeb.Services
{
    public class StockCount
    {
        public StockCount()
        {
            RfidEventLog = new EventLog();
        }

        public int StockCountId { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public EventLog RfidEventLog { get; set; }
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

    public class StockTake
    {
        public int LocationId { get; set; }

        public WorkArea WorkArea { get; set; }

        public List<EpcProduct> ProductIdentifiers { get; set; }
    }

    public class EpcProduct
    {
        public string TagIdHex { get; set; }
    }

    public class EventLog
    {
        public EventLog()
        {
            RfidEvents = new List<RfidEvent>();
        }
        public List<RfidEvent> RfidEvents { get; set; }
    }

    public class RfidEvent
    {
        public int LocationId { get; set; }

        public WorkArea WorkArea { get; set; }

        public string TagIdHex { get; set; }
    }

    public enum WorkArea
    {
        Warehouse,
        Shopfloor,
        Reserved
    }
}