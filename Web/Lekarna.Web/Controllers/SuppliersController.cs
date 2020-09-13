namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SuppliersController : Controller
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
