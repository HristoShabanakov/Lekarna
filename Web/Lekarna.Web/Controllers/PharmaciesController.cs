namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Mvc;

    public class PharmaciesController : Controller
    {
        private readonly IPharmaciesService pharmaciesService;

        public PharmaciesController(IPharmaciesService pharmaciesService)
        {
            this.pharmaciesService = pharmaciesService;
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

            await this.pharmaciesService.CreateAsync(inputModel.Name, inputModel.Country, inputModel.Address, inputModel.ImageUrl);
            return this.View();
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
            return this.View(viewModel);
        }

    }
}
