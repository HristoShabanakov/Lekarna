namespace Lekarna.Web.ViewModels.Offers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferOrderInputModel : IMapTo<Order>
    {
        public string OfferId { get; set; }

        public string MedicineId { get; set; }

        public string PharmacyId { get; set; }

        public string Medicine { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
