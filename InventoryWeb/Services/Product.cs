using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWeb.Services
{
    public class Product
    {
        public int Id { get; set; }
        public string PureIdentity { get; set; }
        public string TPNB { get; set; }
        public string EAN { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}