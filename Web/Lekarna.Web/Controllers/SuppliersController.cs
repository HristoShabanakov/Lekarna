namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class SuppliersController : Controller
    {
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

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Suppliers = this.suppliersService.GetAll<IndexSupplierViewModel>(),
            };

            foreach (var supplier in viewModel.Suppliers)
            {
                supplier.ImageUrl = supplier.ImageUrl == null
                ? "/images/logo.png"
                : this.imagePathPrefix + supplier.ImageUrl;
            }

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
            return this.RedirectToAction("Details", new { id = supplierId });
        }

        public IActionResult ByCompany(string name)
        {
          var viewModel = this.suppliersService.GetByName<SupplierViewModel>(name);

          return this.View(viewModel);
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
