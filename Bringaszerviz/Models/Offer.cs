﻿using System;
using System.Data.Entity;

namespace Bringaszerviz.Models
{
    public class Offer
    {
        public int offerID { get; set; }
        public int ticketID { get; set; }
        public int serviceID { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
        public float price { get; set; }
    }
}