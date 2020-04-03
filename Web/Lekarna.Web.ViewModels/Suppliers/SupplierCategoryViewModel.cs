namespace Lekarna.Web.ViewModels.Suppliers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class SupplierCategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }
    }
}
