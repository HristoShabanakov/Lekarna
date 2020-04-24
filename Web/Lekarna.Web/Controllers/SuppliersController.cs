namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Authorize]
    public class SuppliersController : Controller
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
