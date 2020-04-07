namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class PharmaciesController : Controller
    {
        private readonly IPharmaciesService pharmaciesService;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly string imagePathPrefix;
        private readonly string cloudinaryPrefix = "https://res.cloudinary.com/{0}/image/upload/";

        public PharmaciesController(
            IPharmaciesService pharmaciesService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            this.pharmaciesService = pharmaciesService;
            this.configuration = configuration;
            this.userManager = userManager;
            this.imagePathPrefix = string.Format(this.cloudinaryPrefix, this.configuration["Cloudinary:CloudName"]);
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
            var pharmacyId = await this.pharmaciesService.CreateAsync(inputModel, user);

            return this.RedirectToAction("Details", new { id = pharmacyId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = this.pharmaciesService.GetById<PharmacyDetailsViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error, Home");
            }

            viewModel.ImageUrl = viewModel.ImageUrl == null
                ? "/images/logo.png"
                : this.imagePathPrefix + viewModel.ImageUrl;

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var offerViewModel = this.pharmaciesService.GetById<PharmacyViewModel>(id);
            if (offerViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(offerViewModel);
        }

        public IActionResult Index()
        {
            var viewModel = new AllPharmaciesViewModel
            {
                Pharmacies = this.pharmaciesService.GetAll<PharmacyViewModel>(),
            };

            foreach (var pharmacy in viewModel.Pharmacies)
            {
                pharmacy.ImageUrl = pharmacy.ImageUrl == null
                ? "/images/logo.png"
                : this.imagePathPrefix + pharmacy.ImageUrl;
            }

            return this.View(viewModel);
        }
    }
}
