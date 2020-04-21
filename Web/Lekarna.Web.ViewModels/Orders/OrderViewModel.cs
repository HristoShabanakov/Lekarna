namespace Lekarna.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
