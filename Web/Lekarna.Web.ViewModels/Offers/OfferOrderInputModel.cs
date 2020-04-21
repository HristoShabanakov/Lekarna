namespace Lekarna.Web.ViewModels.Offers
{
    public class OfferOrderInputModel
    {
        public string OfferId { get; set; }

        public string MedicineId { get; set; }

        public string Medicine { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
