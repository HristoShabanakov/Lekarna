namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OffersController : Controller
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ICategoriesService categoriesService;
        private readonly IMedicinesService medicinesService;

        public OffersController(
            IOffersService offersService,
            ICategoriesService categoriesService,
            IMedicinesService medicinesService)
        {
            this.offersService = offersService;
            this.categoriesService = categoriesService;
            this.medicinesService = medicinesService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = await this.offersService
                .GetAllOffersAsync<OfferViewModel>(OffersPerPage, (page - 1) * OffersPerPage);

            var offersCount = await this.offersService.GetAllOffersCountAsync();

            var allOffersViewModel = new AllOffersViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)offersCount / OffersPerPage),
                Offers = viewModel,
            };

            return this.View(allOffersViewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.offersService.GetByIdAsync<OfferViewModel>(id);
            var categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();
            var medicines = await this.medicinesService.GetAllMedicines<MedicineViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Categories = categories;
            viewModel.Medicines = medicines;

            return this.View(viewModel);
        }
    }
}
