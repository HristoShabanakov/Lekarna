namespace Lekarna.Web.ViewModels.Offers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferDeleteViewModel : IMapFrom<Offer>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
