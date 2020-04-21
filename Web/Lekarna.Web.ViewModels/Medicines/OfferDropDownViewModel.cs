namespace Lekarna.Web.ViewModels.Medicines
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferDropDownViewModel : IMapFrom<Offer>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
