namespace Lekarna.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OffersController : Controller
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ISuppliersService suppliersService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public OffersController(
            IOffersService offersService,
            ISuppliersService suppliersService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.suppliersService = suppliersService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult All(int page = 1)
        {
            var viewModel = this.offersService.GetAllOffers<OfferViewModel>(OffersPerPage, (page - 1) * OffersPerPage);

            var offersCount = this.offersService.GetAllOffersCount();

            var allOffersViewModel = new AllOffersViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double)offersCount / OffersPerPage),
                Offers = viewModel,
            };

            return this.View(allOffersViewModel);
        }

        public IActionResult Create()
        {
            var suppliers = this.suppliersService.GetAll<SupplierDropDownViewModel>();
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new OfferCreateInputModel
            {
                Suppliers = suppliers,
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var offerId = await this.offersService.CreateAsync(inputModel, user);
            this.TempData["Notification"] = "Offer was successfully created!";
            return this.RedirectToAction(nameof(this.ById), new { id = offerId });
        }

        public IActionResult ById(string id)
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var offerViewModel = this.offersService.GetById<OfferViewModel>(id);

            if (offerViewModel == null)
            {
                return this.NotFound();
            }

            offerViewModel.Categories = categories;

            return this.View(offerViewModel);
        }
    }
}
