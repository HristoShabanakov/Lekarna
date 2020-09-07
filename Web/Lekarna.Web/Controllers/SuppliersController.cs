namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using static Lekarna.Common.GlobalConstants;

    [Authorize]
    public class SuppliersController : Controller
    {
        private const int SuppliersPerPage = 9;

        private readonly ISuppliersService suppliersService;
        private readonly IConfiguration configuration;

        private readonly string imagePathPrefix;

        public SuppliersController(ISuppliersService suppliersService, IConfiguration configuration)
        {
            this.suppliersService = suppliersService;
            this.configuration = configuration;
            this.imagePathPrefix = string.Format(Cloudinary.Prefix, this.configuration[Cloudinary.CloudName]);
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var skipPages = (page - 1) * SuppliersPerPage;
            var viewModel = await this.suppliersService.GetAllSuppliers<SupplierViewModel>(SuppliersPerPage, skipPages);

            foreach (var supplier in viewModel)
            {
                supplier.ImageUrl = supplier.ImageUrl == null
                ? Images.LogoPath
                : this.imagePathPrefix + supplier.ImageUrl;
            }

            var suppliersCount = await this.suppliersService.GetAllSuppliersCount();

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
            var viewModel = await this.suppliersService.GetById<SupplierViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            return this.View(viewModel);
        }
    }
}
