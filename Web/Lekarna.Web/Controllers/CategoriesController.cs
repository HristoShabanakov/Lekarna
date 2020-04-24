namespace Lekarna.Web.Controllers
{
    using System;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CategoriesController : Controller
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

        public IActionResult All(int page = 1)
        {
            var viewModel = this.categoriesService.GetAllCategories<CategoryViewModel>(CategoriesPerPage, (page - 1) * CategoriesPerPage);

            var categoriesCount = this.categoriesService.GetAllCategoriesCount();

            var allCategoriesViewModel = new AllCategoriesViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)categoriesCount / CategoriesPerPage),
                Categories = viewModel,
            };

            return this.View(allCategoriesViewModel);
        }

        public IActionResult Details(string id)
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
