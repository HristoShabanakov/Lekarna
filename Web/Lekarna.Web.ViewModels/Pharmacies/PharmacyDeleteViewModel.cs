namespace Lekarna.Web.ViewModels.Pharmacies
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class PharmacyDeleteViewModel : IMapFrom<Pharmacy>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
