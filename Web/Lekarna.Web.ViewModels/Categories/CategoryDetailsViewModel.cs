namespace Lekarna.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class CategoryDetailsViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }
    }
}
