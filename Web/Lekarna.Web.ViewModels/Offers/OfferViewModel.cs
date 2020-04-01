namespace Lekarna.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferViewModel : IMapFrom<Offer>
    {
        public string Name { get; set; }

        public string Medicine { get; set; }

        public decimal Price { get; set; }

        public int Target { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
