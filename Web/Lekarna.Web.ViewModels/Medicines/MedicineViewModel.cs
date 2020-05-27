namespace Lekarna.Web.ViewModels.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class MedicineViewModel : IMapFrom<Medicine>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        [Required]
        public string TargetId { get; set; }

        [Required]
        public string DiscountId { get; set; }

        public string OfferId { get; set; }

        public IEnumerable<OfferDropDownViewModel> Offers { get; set; }
    }
}
