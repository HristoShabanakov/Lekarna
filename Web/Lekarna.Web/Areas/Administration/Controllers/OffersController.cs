namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static Lekarna.Common.GlobalConstants;

    public class OffersController : AdministrationController
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ISuppliersService suppliersService;
        private readonly ICategoriesService categoriesService;
        private readonly IMedicinesService medicinesService;
        private readonly ITargetsService targetsService;
        private readonly IDiscountsService discountsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OffersController(
            IOffersService offersService,
            ISuppliersService suppliersService,
            ICategoriesService categoriesService,
            IMedicinesService medicinesService,
            ITargetsService targetsService,
            IDiscountsService discountsService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.suppliersService = suppliersService;
            this.categoriesService = categoriesService;
            this.medicinesService = medicinesService;
            this.targetsService = targetsService;
            this.discountsService = discountsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = await this.offersService.GetAllOffers<OfferViewModel>(OffersPerPage, (page - 1) * OffersPerPage);

            var offersCount = await this.offersService.GetAllOffersCount();

            var allOffersViewModel = new AllOffersViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)offersCount / OffersPerPage),
                Offers = viewModel,
            };

            return this.View(allOffersViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var suppliers = await this.suppliersService.GetAll<SupplierDropDownViewModel>();
            var categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new OfferCreateInputModel
            {
                Suppliers = suppliers,
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();
                var suppliers = await this.suppliersService.GetAll<SupplierDropDownViewModel>();
                var viewModel = new OfferCreateInputModel
                {
                    Suppliers = suppliers,
                    Categories = categories,
                    Data = inputModel.Data,
                };
                return this.View(viewModel);
            }

            var offerId = await this.offersService
                .CreateAsync(inputModel.Name, inputModel.SupplierId, inputModel.CategoryId, inputModel.Data);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyCreatedOffer;
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.offersService.GetById<OfferViewModel>(id);

            viewModel.Suppliers = await this.suppliersService.GetAll<SupplierDropDownViewModel>();
            viewModel.Categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var offerId = await this.offersService
                .EditAsync(inputModel.Id, inputModel.Name, inputModel.CategoryId, inputModel.SupplierId);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyEditedOffer;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.offersService.GetById<OfferDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            var offerId = await this.offersService.DeleteAsync(id);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyDeletedOffer;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.offersService.GetById<OfferViewModel>(id);
            var categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();
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
