namespace Lekarna.Web.ViewModels.Offers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class SupplierDropDownViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
