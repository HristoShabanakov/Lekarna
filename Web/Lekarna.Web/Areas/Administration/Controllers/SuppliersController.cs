namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class SuppliersController : AdministrationController
    {
        private const int SuppliersPerPage = 9;

        private readonly ISuppliersService suppliersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        private readonly string imagePathPrefix;
        private readonly string cloudinaryPrefix = "https://res.cloudinary.com/{0}/image/upload/";

        public SuppliersController(
            ISuppliersService suppliersService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            this.suppliersService = suppliersService;
            this.userManager = userManager;
            this.configuration = configuration;
            this.imagePathPrefix = string.Format(this.cloudinaryPrefix, this.configuration["Cloudinary:CloudName"]);
        }

        public IActionResult All(int page = 1)
        {
            var viewModel = this.suppliersService.GetAllSuppliers<SupplierViewModel>(SuppliersPerPage, (page - 1) * SuppliersPerPage);

            foreach (var supplier in viewModel)
            {
                supplier.ImageUrl = supplier.ImageUrl == null
                ? "/images/logo.png"
                : this.imagePathPrefix + supplier.ImageUrl;
            }

            var suppliersCount = this.suppliersService.GetAllSuppliersCount();

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

            var user = await this.userManager.GetUserAsync(this.User);
            var supplierId = await this.suppliersService.CreateAsync(inputModel, user);

            if (supplierId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Supplier was successfully created!";
            return this.RedirectToAction("Details", new { id = supplierId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = this.suppliersService.GetById<SupplierViewModel>(id);

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
        public async Task<IActionResult> Edit(SupplierEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var supplierId = await this.suppliersService.EditAsync(inputModel);

            if (supplierId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Supplier was successfully edited!";

            return this.RedirectToAction("Details", new { id = supplierId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = this.suppliersService.GetById<SupplierDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

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

            // validations
            this.TempData["Notification"] = "Supplier was successfully deleted!";

            return this.RedirectToAction("All");
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.suppliersService.GetById<SupplierViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            return this.View(viewModel);
        }
    }
}
