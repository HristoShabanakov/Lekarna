namespace Lekarna.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Suppliers;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public IEnumerable<SuppliersOffersViewModel> Offers { get; set; }
    }
}
