namespace Lekarna.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class AllCategoriesViewModel : IMapFrom<Category>
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
