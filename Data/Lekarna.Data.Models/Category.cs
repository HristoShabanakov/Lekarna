namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Offers = new HashSet<Offer>();
        }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
