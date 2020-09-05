namespace Lekarna.Data.Models
{
    using System;

    using Lekarna.Data.Common.Models;

    public class Order : BaseDeletableStringIdModel
    {
        public DateTime IssuedOn { get; set; }

        public string PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }

        public string OrderItemId { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
