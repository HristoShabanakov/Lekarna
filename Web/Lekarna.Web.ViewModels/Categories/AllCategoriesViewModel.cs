namespace Lekarna.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class AllCategoriesViewModel : IMapFrom<Category>
    {
        public AllCategoriesViewModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
