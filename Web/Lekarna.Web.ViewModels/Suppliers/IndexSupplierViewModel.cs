namespace Lekarna.Web.ViewModels.Suppliers
{
    using System;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class IndexSupplierViewModel : IMapFrom<Supplier>
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public int OffersCount { get; set; }

        public string Url => $"{this.Name.Replace(' ', '-')}";
    }
}
