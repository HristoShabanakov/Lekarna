namespace Lekarna.Web.ViewModels.Offers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferCreateInputModel : IMapFrom<Category>
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Medicine { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Target { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public string CategoryCategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }
    }
}
