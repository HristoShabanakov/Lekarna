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

    public class PharmaciesController : AdministrationController
    {
        private const int PharmaciesPerPage = 9;

        private readonly IPharmaciesService pharmaciesService;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly string imagePathPrefix;
        private readonly string cloudinaryPrefix = "https://res.cloudinary.com/{0}/image/upload/";

        public PharmaciesController(
            IPharmaciesService pharmaciesService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            this.pharmaciesService = pharmaciesService;
            this.configuration = configuration;
            this.userManager = userManager;
            this.imagePathPrefix = string.Format(this.cloudinaryPrefix, this.configuration["Cloudinary:CloudName"]);
        }

        public IActionResult All(int page = 1)
        {
            var viewModel = this.pharmaciesService.GetAllPharmacies<PharmacyViewModel>(PharmaciesPerPage, (page - 1) * PharmaciesPerPage);

            foreach (var pharmacy in viewModel)
            {
                pharmacy.ImageUrl = pharmacy.ImageUrl == null
                ? "/images/logo.png"
                : this.imagePathPrefix + pharmacy.ImageUrl;
            }

            var pharmaciesCount = this.pharmaciesService.GetAllPharmaciesCount();

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
            var pharmacyId = await this.pharmaciesService.CreateAsync(inputModel, user);

            if (pharmacyId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Pharmacy was successfully created!";
            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = this.pharmaciesService.GetById<PharmacyDetailsViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            viewModel.ImageUrl = viewModel.ImageUrl == null
                ? "/images/logo.png"
               : this.imagePathPrefix + viewModel.ImageUrl;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = this.pharmaciesService.GetById<PharmacyDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ImageUrl = viewModel.ImageUrl == null
                ? "/images/logo.png"
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

            var pharmacyId = await this.pharmaciesService.EditAsync(inputModel);

            if (pharmacyId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Pharmacy was successfully edited!";

            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = this.pharmaciesService.GetById<PharmacyDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

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

            this.TempData["Notification"] = "Pharmacy was successfully deleted!";

            return this.RedirectToAction("All");
        }

        public IActionResult ById(string id)
        {
            var offerViewModel = this.pharmaciesService.GetById<PharmacyViewModel>(id);
            if (offerViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(offerViewModel);
        }
    }
}
