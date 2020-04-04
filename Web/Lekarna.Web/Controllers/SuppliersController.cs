﻿namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IOffersService offersService;

        public SuppliersController(
            ISuppliersService suppliersService,
            UserManager<ApplicationUser> userManager,
            IOffersService offersService)
        {
            this.suppliersService = suppliersService;
            this.userManager = userManager;
            this.offersService = offersService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Suppliers = this.suppliersService.GetAll<IndexSupplierViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new SupplierCreateInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var supplierId = await this.suppliersService.CreateAsync(inputModel, user);
            this.TempData["Notification"] = "Supplier was successfully created!";
            return this.RedirectToAction("Index");
        }

        public IActionResult ByCompany(string name)
        {
          var viewModel = this.suppliersService.GetByName<SupplierViewModel>(name);

          return this.View(viewModel);
        }
    }
}
