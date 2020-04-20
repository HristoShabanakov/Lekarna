namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

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
