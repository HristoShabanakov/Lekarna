namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static Lekarna.Common.GlobalConstants;

    [Authorize]
    public class PharmaciesController : Controller
    {
        private const int PharmaciesPerPage = 3;

        private readonly IPharmaciesService pharmaciesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PharmaciesController(IPharmaciesService pharmaciesService, UserManager<ApplicationUser> userManager)
        {
            this.pharmaciesService = pharmaciesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.pharmaciesService
                .GetAllPharmaciesAsync<PharmacyViewModel>(user.Id, PharmaciesPerPage, (page - 1) * PharmaciesPerPage);

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
                return this.RedirectToAction("NotFound, Errors");
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.pharmaciesService.GetByIdAsync<PharmacyDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("NotFound", "Errors");
            }

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
                return this.RedirectToAction("BadRequest", "Errors");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyEditedPharmacy;

            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.pharmaciesService.GetByIdAsync<PharmacyDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("NotFound", "Errors");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePharmacy(string id)
        {
            var pharmacyId = await this.pharmaciesService.DeleteAsync(id);

            if (pharmacyId == null)
            {
                return this.RedirectToAction("BadRequest", "Errors");
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
