namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Mvc;

    using static Lekarna.Common.GlobalConstants;

    public class OffersController : AdministrationController
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ISuppliersService suppliersService;
        private readonly ICategoriesService categoriesService;
        private readonly IMedicinesService medicinesService;

        public OffersController(
            IOffersService offersService,
            ISuppliersService suppliersService,
            ICategoriesService categoriesService,
            IMedicinesService medicinesService)
        {
            this.offersService = offersService;
            this.suppliersService = suppliersService;
            this.categoriesService = categoriesService;
            this.medicinesService = medicinesService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = await this.offersService.GetAllOffersAsync<OfferViewModel>(OffersPerPage, (page - 1) * OffersPerPage);

            var offersCount = await this.offersService.GetAllOffersCountAsync();

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
            var suppliers = await this.suppliersService.GetAllAsync<SupplierDropDownViewModel>();
            var categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();
            var viewModel = new OfferCreateInputModel
            {
                Suppliers = suppliers,
                Categories = categories,
                ExpirationDate = DateTime.Now.Date,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateInputModel inputModel)
        {
            var invalidDate = inputModel.ExpirationDate.Date.CompareTo(DateTime.Now.Date) <= 0;

            if (!this.ModelState.IsValid || invalidDate)
            {
                var categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();
                var suppliers = await this.suppliersService.GetAllAsync<SupplierDropDownViewModel>();
                var viewModel = new OfferCreateInputModel
                {
                    Suppliers = suppliers,
                    Categories = categories,
                    Data = inputModel.Data,
                    ExpirationDate = inputModel.ExpirationDate,
                };

                this.SetInvalidExpirationDateError(invalidDate);

                return this.View(viewModel);
            }

            var offerId = await this.offersService
                .CreateAsync(inputModel.Name, inputModel.SupplierId, inputModel.CategoryId, inputModel.ExpirationDate, inputModel.Data);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyCreatedOffer;
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.offersService.GetByIdAsync<OfferEditViewModel>(id);

            viewModel.Suppliers = await this.suppliersService.GetAllAsync<SupplierDropDownViewModel>();
            viewModel.Categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferEditViewModel inputModel)
        {
            var invalidDate = inputModel.ExpirationDate.Date.CompareTo(DateTime.Now.Date) <= 0;

            if (!this.ModelState.IsValid || invalidDate)
            {
                var categories = await this.categoriesService.GetAllAsync<CategoryDropDownViewModel>();
                var suppliers = await this.suppliersService.GetAllAsync<SupplierDropDownViewModel>();
                var viewModel = new OfferEditViewModel
                {
                    Suppliers = suppliers,
                    Categories = categories,
                    ExpirationDate = inputModel.ExpirationDate,
                };

                this.SetInvalidExpirationDateError(invalidDate);
                return this.View(viewModel);
            }

            var offerId = await this.offersService
                .EditAsync(inputModel.Id, inputModel.Name, inputModel.CategoryId, inputModel.SupplierId, inputModel.ExpirationDate);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyEditedOffer;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.offersService.GetByIdAsync<OfferDeleteViewModel>(id);

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

        private void SetInvalidExpirationDateError(bool invalidDate)
        {
            if (invalidDate)
            {
                this.ViewData["DateError"] = Offer.InvalidExpirationDateError;
            }
        }
    }
}
