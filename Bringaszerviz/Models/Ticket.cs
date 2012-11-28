using System;
using System.Data.Entity;

namespace Bringaszerviz.Models
{
    public class Ticket
    {
        public int ticketID { get; set; }
        public int ownerID { get; set; }
        public string description { get; set; }
    }
}