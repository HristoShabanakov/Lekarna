namespace Lekarna.Web.ViewModels.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class SupplierDetailsViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Address { get; set; }

        public string UserId { get; set; }

        public string UserUsername { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
