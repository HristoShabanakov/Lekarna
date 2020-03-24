namespace Lekarna.Web.ViewModels.Offers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OfferCreateInputModel
    {
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

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }
    }
}
