namespace Lekarna.Web.ViewModels.Suppliers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class SuppliersOffersViewModel : IMapFrom<Offer>
    {
        public string Name { get; set; }

        public string Medicine { get; set; }

        public string UserUsername { get; set; }

        public decimal Price { get; set; }

        public int Target { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }
    }
}
