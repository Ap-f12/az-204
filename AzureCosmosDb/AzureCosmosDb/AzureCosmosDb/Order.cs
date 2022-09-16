using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCosmosDb
{
    internal class Order
    {
        public string id { get; set; }
        public string OrderId { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
