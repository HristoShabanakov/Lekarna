namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CategoriesController : Controller
    {
        private const int CategoriesPerPage = 10;

        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
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
