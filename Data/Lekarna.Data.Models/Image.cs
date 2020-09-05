namespace Lekarna.Data.Models
{
    using Lekarna.Data.Common.Models;

    public class Image : BaseDeletableStringIdModel
    {
        public string Url { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
