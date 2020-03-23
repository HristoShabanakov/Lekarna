namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OffersController : Controller
    {
        private readonly IOffersService offersService;
        private readonly ISuppliersService suppliersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OffersController(
            IOffersService offersService,
            ISuppliersService suppliersService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.suppliersService = suppliersService;
            this.userManager = userManager;
        }

        public IActionResult ById(string id)
        {
            var suppliers = this.suppliersService.GetAll<SupplierDropDownViewModel>();
            var viewModel = new OfferCreateInputModel
            {
                Suppliers = suppliers,
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OfferCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var offerId = await this.offersService.CreateAsync(
                inputModel.Name,
                inputModel.Medicine,
                inputModel.Price,
                inputModel.SupplierId,
                user.Id);
            return this.RedirectToAction(nameof(this.ById), new { id = offerId });
        }
    }
}
