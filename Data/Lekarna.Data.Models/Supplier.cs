namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Common.Models;

    public class Supplier : BaseDeletableModel<string>
    {
        public Supplier()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Offers = new HashSet<Offer>();
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
