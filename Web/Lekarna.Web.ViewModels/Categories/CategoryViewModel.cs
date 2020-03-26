namespace Lekarna.Web.ViewModels.Categories
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
