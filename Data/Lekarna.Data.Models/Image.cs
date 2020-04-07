namespace Lekarna.Data.Models
{
    using System;

    using Lekarna.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
