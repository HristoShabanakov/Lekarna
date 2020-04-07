namespace Lekarna.Web.ViewModels.Suppliers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class SupplierCreateInputModel : IMapFrom<Image>
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public string SupplierId { get; set; }

        public string Url { get; set; }

        public string ImageId { get; set; }
    }
}
