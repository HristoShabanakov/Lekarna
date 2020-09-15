namespace Lekarna.Data.Models
{
    using System.Collections.Generic;

    using Lekarna.Data.Common.Enumerations;
    using Lekarna.Data.Common.Models;

    public class Order : BaseDeletableStringIdModel
    {
        public Order()
        {
            this.OrdersItems = new HashSet<OrderItem>();
        }

        public string PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public Status Status { get; set; }

        public string OfferId { get; set; }

        public Offer Offer { get; set; }

        public ICollection<OrderItem> OrdersItems { get; set; }
    }
}
