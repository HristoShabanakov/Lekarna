namespace Lekarna.Web.ViewModels.Offers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class OfferCreateInputModel : IMapFrom<Offer>
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }

        [Required]
        public IFormFile Data { get; set; }
    }
}
