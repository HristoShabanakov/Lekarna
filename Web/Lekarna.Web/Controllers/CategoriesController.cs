namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

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
            var viewModel = new CategoryCreateInputModel();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var categoryId = await this.categoriesService.CreateAsync(
                inputModel.CategoryName,
                inputModel.Description);
            this.TempData["Notification"] = "Category was successfully created!";
            return this.RedirectToAction(nameof(this.All), new { id = categoryId });
        }

        public IActionResult All()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var categoriesViewModel = this.categoriesService.GetById<CategoryViewModel>(id);

            if (categoriesViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(categoriesViewModel);
        }
    }
}
