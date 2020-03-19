namespace Lekarna.Data.Models
{
    using Lekarna.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
