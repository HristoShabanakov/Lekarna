namespace Lekarna.Web.ViewModels.Suppliers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class SupplierDeleteViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
