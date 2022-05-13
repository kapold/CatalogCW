using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    public class Delivery
    {
        public int DeliveryID { get; set; }
        public int OrderID { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryAddress { get; set; }
        //public string? DeliveryType { get; set; }
        public string? PaymentType { get; set; }
        public Order? Order { get; set; }
        public Delivery? DeliveryLink { get; set; }

        public Delivery()
        {
            Order = new Order();
        }
    }
}
