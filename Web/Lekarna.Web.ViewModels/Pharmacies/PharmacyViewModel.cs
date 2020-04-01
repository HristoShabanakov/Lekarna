namespace Lekarna.Web.ViewModels.Pharmacies
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class PharmacyViewModel : IMapFrom<Pharmacy>
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }
    }
}
