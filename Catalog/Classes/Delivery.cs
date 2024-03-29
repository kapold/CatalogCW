﻿using System;
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
        public DateOnly DeliveryDate { get; set; }
        public string? DeliveryAddress { get; set; }
        //public string? DeliveryType { get; set; }
        public string? PaymentType { get; set; }
        public int DeliveryCount { get; set; }
        public Order? Order { get; set; }
        public Delivery? DeliveryLink { get; set; }
        public User? Customer { get; set; }

        public Delivery()
        {
            Order = new Order();
            Customer = new User();
        }

        public override string ToString()
        {
            return $"DeliveryID: {DeliveryID}\n" +
                $"OrderID: {OrderID}\n" +
                $"DeliveryDate: {DeliveryDate}\n" +
                $"DeliveryAddress: {DeliveryAddress}\n" +
                $"PaymentType: {PaymentType}\n" +
                $"DeliveryCount: {DeliveryCount}\n";
        }
    }
}
