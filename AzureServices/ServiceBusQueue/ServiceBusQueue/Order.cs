﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQueue
{
    public class Order
    {
        public string OrderId { get; set; }
        public float UnitPrice { get; set; }
        
        public int Quantity { get; set; }
    }
}
