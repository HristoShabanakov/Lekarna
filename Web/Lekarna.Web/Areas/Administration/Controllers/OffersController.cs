namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OffersController : AdministrationController
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
                var suppliers = this.suppliersService.GetAll<SupplierDropDownViewModel>();
                var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
                var viewModel = new OfferCreateInputModel
                {
                    Suppliers = suppliers,
                    Categories = categories,
                };
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var offerId = await this.offersService.CreateAsync(inputModel);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Offer was successfully created!";
            return this.RedirectToAction(nameof(this.Details), new { id = offerId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = this.offersService.GetById<OfferViewModel>(id);

            viewModel.Suppliers = this.suppliersService.GetAll<SupplierDropDownViewModel>();
            viewModel.Categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", new { id = inputModel.Id });
            }

            var offerId = await this.offersService.EditAsync(inputModel);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Offer was successfully edited!";

            return this.RedirectToAction("Details", new { id = offerId });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = this.offersService.GetById<OfferDeleteViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            var offerId = await this.offersService.DeleteAsync(id);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Offer was successfully deleted!";

            return this.RedirectToAction("All");
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.offersService.GetById<OfferViewModel>(id);
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Categories = categories;

            return this.View(viewModel);
        }
    }
}
