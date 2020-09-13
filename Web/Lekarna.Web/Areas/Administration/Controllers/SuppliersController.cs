namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    using static Lekarna.Common.GlobalConstants;

    public class SuppliersController : AdministrationController
    {
        private const int SuppliersPerPage = 9;

        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var skipPages = (page - 1) * SuppliersPerPage;
            var viewModel = await this.suppliersService.GetAllSuppliersAsync<SupplierViewModel>(SuppliersPerPage, skipPages);

            var suppliersCount = await this.suppliersService.GetAllSuppliersCountAsync();

            var suppliersAllViewModel = new SuppliersAllViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)suppliersCount / SuppliersPerPage),
                Suppliers = viewModel,
            };

            return this.View(suppliersAllViewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new SupplierCreateViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var supplierId = await this
                .suppliersService
                .CreateAsync(inputModel.Name, inputModel.Country, inputModel.Address, inputModel.NewImage);

            if (supplierId == null)
            {
                this.TempData[Notifications.Error] = Notifications.SuplierAlreadyExists;
                return this.View(inputModel);
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyCreatedSupplier;
            return this.RedirectToAction("Details", new { id = supplierId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.suppliersService.GetByIdAsync<SupplierViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var supplierId = await this
                .suppliersService
                .EditAsync(inputModel.Name, inputModel.Country, inputModel.Address, inputModel.NewImage, inputModel.Id);

            if (supplierId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyEditedSupplier;

            return this.RedirectToAction("Details", new { id = supplierId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.suppliersService.GetByIdAsync<SupplierDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSupplier(string id)
        {
            var supplierId = await this.suppliersService.DeleteAsync(id);

            if (supplierId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData[Notifications.Key] = Notifications.SuccessfullyDeletedSupplier;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.suppliersService.GetByIdAsync<SupplierViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            return this.View(viewModel);
        }
    }
}
