using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace Bringaszerviz.Models
{
    public class Ticket
    {
        public int ticketID { get; set; }
        public int ownerID { get; set; }
        public virtual List<Offer> offers { get; set;}
        public string description { get; set; }
        public Boolean solved { get; set; }
    }
}