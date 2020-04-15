namespace Lekarna.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferViewModel : IMapFrom<Offer>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Medicine { get; set; }

        public decimal Price { get; set; }

        public int Target { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public string SupplierId { get; set; }

        public string CategoryCategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }
    }
}
