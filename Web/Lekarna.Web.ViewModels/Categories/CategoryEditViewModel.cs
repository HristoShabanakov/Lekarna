namespace Lekarna.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class CategoryEditViewModel : IMapTo<Category>
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
