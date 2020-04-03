namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Medicine { get; set; }

        public decimal Price { get; set; }

        public int Target { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public int OffersCount { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
