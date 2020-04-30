namespace Lekarna.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OrderCreateViewModel : IMapFrom<Offer>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string OfferId { get; set; }

        public string MedicineId { get; set; }
    }
}
