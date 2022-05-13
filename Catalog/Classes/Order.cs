using System;
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
        public bool IsOrdered { get; set; }
        public int Customer { get; set; }

        public Good? Good { get; set; }
        public Order? OrderLink { get; set; }

        public Order()
        {
            Good = new Good();
        }

        public override string ToString()
        {
            return $"OrderNo: {OrderNo}\n" +
                $"GoodID: {GoodID}\n" +
                $"GoodCount: {GoodCount}\n" +
                $"IsOrdered: {IsOrdered}\n" +
                $"Customer: {Customer}\n";
        }
    }
}
