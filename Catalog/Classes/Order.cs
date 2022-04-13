﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    public class Order
    {
        public int OrderNo { get; set; }
        public int GoodID { get; set; }
        public int GoodCount { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int Customer { get; set; }
    }
}