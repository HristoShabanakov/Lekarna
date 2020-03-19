namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Common.Models;

    public class Pharmacy : BaseDeletableModel<string>
    {
        public Pharmacy()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
