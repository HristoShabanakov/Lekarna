namespace Lekarna.Web.Controllers
{
    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IOffersService offersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(
            ICategoriesService categoriesService,
            IOffersService offersService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.offersService = offersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return null;
        }

        public IActionResult All()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
