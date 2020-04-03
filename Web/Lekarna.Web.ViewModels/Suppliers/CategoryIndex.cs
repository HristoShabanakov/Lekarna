namespace Lekarna.Web.ViewModels.Suppliers
{
    using System.Collections.Generic;

    using Lekarna.Web.ViewModels.Categories;

    public class CategoryIndex
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
