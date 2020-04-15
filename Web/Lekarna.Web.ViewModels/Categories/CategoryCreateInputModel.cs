namespace Lekarna.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryCreateInputModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
