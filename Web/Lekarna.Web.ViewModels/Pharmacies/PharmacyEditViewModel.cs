namespace Lekarna.Web.ViewModels.Pharmacies
{

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class PharmacyEditViewModel : IMapTo<Pharmacy>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
