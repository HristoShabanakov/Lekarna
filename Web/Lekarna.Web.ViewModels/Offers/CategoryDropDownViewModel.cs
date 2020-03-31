namespace Lekarna.Web.ViewModels.Offers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string CategoryName { get; set; }
    }
}