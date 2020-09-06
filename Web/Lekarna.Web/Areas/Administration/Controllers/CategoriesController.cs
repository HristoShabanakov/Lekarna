namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static Lekarna.Common.GlobalConstants.Notifications;

    public class CategoriesController : AdministrationController
    {
        private const int CategoriesPerPage = 10;

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

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = await this.categoriesService
                .GetAllCategories<CategoryViewModel>(CategoriesPerPage, (page - 1) * CategoriesPerPage);

            var categoriesCount = await this.categoriesService.GetAllCategoriesCount();

            var allCategoriesViewModel = new AllCategoriesViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)categoriesCount / CategoriesPerPage),
                Categories = viewModel,
            };

            return this.View(allCategoriesViewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CategoryCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var categoryId = await this.categoriesService.CreateAsync(
                inputModel.CategoryName,
                inputModel.Description);

            if (categoryId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Key] = SuccessfullyCreatedCategory;
            return this.RedirectToAction(nameof(this.All), new { id = categoryId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.categoriesService.GetById<CategoryDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var categoryId = await this.categoriesService
                .EditAsync(inputModel.Id, inputModel.CategoryName, inputModel.Description);

            if (categoryId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Key] = SuccessfullyEditedCategory;

            return this.RedirectToAction("Details", new { id = categoryId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.categoriesService.GetById<CategoryDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var categoryId = await this.categoriesService.DeleteAsync(id);

            if (categoryId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Key] = SuccessfullyDeletedCategory;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Details(string id)
        {
            var categoriesViewModel = await this.categoriesService.GetById<CategoryViewModel>(id);

            if (categoriesViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(categoriesViewModel);
        }
    }
}
