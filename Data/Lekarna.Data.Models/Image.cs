namespace Lekarna.Data.Models
{
    using Lekarna.Data.Common.Models;

    public class Image : BaseDeletableStringIdModel
    {
        public string Url { get; set; }

        public string PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public string SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
