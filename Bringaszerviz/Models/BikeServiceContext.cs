using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bringaszerviz.Models
{
    public class BikeServiceContext : DbContext
    {
            public DbSet<UserProfile> UserProfiles { get; set; }
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<Offer> Offers { get; set; }

            public BikeServiceContext()
                : base("DefaultConnection")
            {
                this.Configuration.ProxyCreationEnabled = true;
                this.Configuration.AutoDetectChangesEnabled = true;
            }
    }
}