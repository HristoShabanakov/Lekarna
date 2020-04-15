namespace Lekarna.Web.ViewModels.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class SupplierCreateViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Address { get; set; }

        public string UserId { get; set; }

        public string Url { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }
    }
}
