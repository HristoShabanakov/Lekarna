namespace Lekarna.Web.ViewModels.Suppliers
{
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Categories;

    public class SupplierViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public IEnumerable<SuppliersOffersViewModel> Offers { get; set; }

        public IEnumerable<SupplierCategoryViewModel> Categories { get; set; }
    }
}
