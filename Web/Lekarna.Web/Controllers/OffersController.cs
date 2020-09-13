namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Offers;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OffersController : Controller
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ICategoriesService categoriesService;
        private readonly IMedicinesService medicinesService;
        private readonly IPharmaciesService pharmaciesService;
        private readonly UserManager<ApplicationUser> userManager;

        public OffersController(
            IOffersService offersService,
            ICategoriesService categoriesService,
            IMedicinesService medicinesService,
            IPharmaciesService pharmaciesService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.categoriesService = categoriesService;
            this.medicinesService = medicinesService;
            this.pharmaciesService = pharmaciesService;
            this.userManager = userManager;
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
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = await this.offersService.GetByIdAsync<OfferViewModel>(id);
            var categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();
            var medicines = await this.medicinesService.GetAllMedicines<MedicineViewModel>(id);
            var pharmacies = await this.pharmaciesService.GetAllPharmaciesAsync<PharmacyViewModel>(user.Id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Categories = categories;
            viewModel.Medicines = medicines;
            viewModel.Pharmacies = pharmacies;

            return this.View(viewModel);
        }
    }
}
