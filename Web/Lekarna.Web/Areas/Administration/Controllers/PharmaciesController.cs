namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using static Lekarna.Common.GlobalConstants;

    public class PharmaciesController : AdministrationController
    {
        private const int PharmaciesPerPage = 9;

        private readonly IPharmaciesService pharmaciesService;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly string imagePathPrefix;

        public PharmaciesController(
            IPharmaciesService pharmaciesService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            this.pharmaciesService = pharmaciesService;
            this.configuration = configuration;
            this.userManager = userManager;
            this.imagePathPrefix = string.Format(Cloudinary.Prefix, this.configuration["Cloudinary:CloudName"]);
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = await this.pharmaciesService.GetAllPharmaciesAsync<PharmacyViewModel>(null, PharmaciesPerPage, (page - 1) * PharmaciesPerPage);

            foreach (var pharmacy in viewModel)
            {
                pharmacy.ImageUrl = pharmacy.ImageUrl == null
                ? Images.LogoPath
                : this.imagePathPrefix + pharmacy.ImageUrl;
            }

            var pharmaciesCount = await this.pharmaciesService.GetAllPharmaciesCountAsync();

            var allPharmaciesViewModel = new AllPharmaciesViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)pharmaciesCount / PharmaciesPerPage),
                Pharmacies = viewModel,
            };

            return this.View(allPharmaciesViewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new PharmacyViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PharmacyViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var pharmacyId = await this
                .pharmaciesService
                .CreateAsync(inputModel.Name, inputModel.Country, inputModel.Address, inputModel.NewImage, user.Id);

            if (string.IsNullOrEmpty(pharmacyId))
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyCreatedPharmacy;
            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.pharmaciesService.GetByIdAsync<PharmacyDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            viewModel.ImageUrl = viewModel.ImageUrl == null
                ? Images.LogoPath
               : this.imagePathPrefix + viewModel.ImageUrl;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.pharmaciesService.GetByIdAsync<PharmacyDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            viewModel.ImageUrl = viewModel.ImageUrl == null
                ? Images.LogoPath
                : this.imagePathPrefix + viewModel.ImageUrl;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PharmacyEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var pharmacyId = await this
                .pharmaciesService
                .EditAsync(inputModel.Name, inputModel.Country, inputModel.Address, inputModel.NewImage, inputModel.Id);

            if (pharmacyId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyEditedPharmacy;

            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.pharmaciesService.GetByIdAsync<PharmacyDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePharmacy(string id)
        {
            var pharmacyId = await this.pharmaciesService.DeleteAsync(id);

            if (pharmacyId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyDeletedPharmacy;

            return this.RedirectToAction("All");
        }

        public IActionResult ById(string id)
        {
            var offerViewModel = this.pharmaciesService.GetByIdAsync<PharmacyViewModel>(id);
            if (offerViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(offerViewModel);
        }
    }
}
