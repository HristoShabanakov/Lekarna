namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Discounts;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Offers;
    using Lekarna.Web.ViewModels.Targets;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OffersController : AdministrationController
    {
        private const int OffersPerPage = 10;

        private readonly IOffersService offersService;
        private readonly ISuppliersService suppliersService;
        private readonly ICategoriesService categoriesService;
        private readonly IMedicinesService medicinesService;
        private readonly ITargetsService targetsService;
        private readonly IDiscountsService discountsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OffersController(
            IOffersService offersService,
            ISuppliersService suppliersService,
            ICategoriesService categoriesService,
            IMedicinesService medicinesService,
            ITargetsService targetsService,
            IDiscountsService discountsService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.suppliersService = suppliersService;
            this.categoriesService = categoriesService;
            this.medicinesService = medicinesService;
            this.targetsService = targetsService;
            this.discountsService = discountsService;
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
            var offerId = await this.offersService.CreateAsync(inputModel);

            if (offerId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var file = inputModel.Data;
            if (!this.ModelState.IsValid)
            {
                var suppliers = this.suppliersService.GetAll<SupplierDropDownViewModel>();
                var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
                var viewModel = new OfferCreateInputModel
                {
                    Suppliers = suppliers,
                    Categories = categories,
                    Data = file,
                };
                return this.View(viewModel);
            }

            string fileExtension = Path.GetExtension(inputModel.Data.FileName);

            var medicinesDbRecords = new List<MedicineViewModel>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string[] headers = reader.ReadLine()
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var textReader = reader.ReadToEnd()
                    .Replace(" лв.", string.Empty).Replace("%", string.Empty)
                    .Replace("\"", string.Empty)
                    .Trim();
                var rows = textReader.Split("\r\n");
                for (int i = 0; i < rows.Length; i++)
                {
                    var cols = rows[i].Split(";");
                    string name = cols[0];
                    string price = cols[1];
                    string target = cols[2];
                    string discount = cols[3];

                    if (name.Contains("Total") && target.Any())
                    {
                        var targetModel = new TargetViewModel
                        {
                            Quantity = int.Parse(cols[2]),
                        };
                        var idTarget = await this.targetsService.CreateAsync(targetModel);

                        for (int index = 0; index < medicinesDbRecords.Count; index++)
                        {
                            medicinesDbRecords[index].TargetId = idTarget;

                            var medicines = medicinesDbRecords[index];
                            var db = await this.medicinesService.CreateAsync(medicines);
                        }

                        medicinesDbRecords.Clear();
                    }

                    if (name.Any() && price.Any() && target.Length == 0 && discount.Any())
                    {
                        var discountDbModel = new DiscountViewModel
                        {
                            Quantity = decimal.Parse(cols[3]),
                        };

                        var discountIdDb = await this.discountsService.CreateAsync(discountDbModel);

                        medicinesDbRecords.Add(new MedicineViewModel
                        {
                            Name = cols[0],
                            Price = decimal.Parse(cols[1]),
                            DiscountId = discountIdDb,
                            OfferId = offerId,
                        });
                    }

                    if (name.Any() && price.Any() && target.Any() && discount.Length == 0)
                    {
                        var targetModel = new TargetViewModel
                        {
                            Quantity = int.Parse(cols[2]),
                        };

                        var idTarget = await this.targetsService.CreateAsync(targetModel);

                        medicinesDbRecords.Add(new MedicineViewModel
                        {
                            Name = cols[0],
                            Price = decimal.Parse(cols[1]),
                            TargetId = idTarget,
                            OfferId = offerId,
                        });
                    }

                    if (name.Contains("FORMULA") && discount.Any())
                    {
                        var discountDbModel = new DiscountViewModel
                        {
                            Quantity = decimal.Parse(cols[3]),
                        };

                        var discountIdDb = await this.discountsService.CreateAsync(discountDbModel);

                        for (int j = 0; j < medicinesDbRecords.Count; j++)
                        {
                            medicinesDbRecords[j].DiscountId = discountIdDb;
                            var medicines = medicinesDbRecords[j];
                            var db = await this.medicinesService.CreateAsync(medicines);
                        }

                        medicinesDbRecords.Clear();
                    }

                    if (cols.Contains(string.Empty))
                    {
                        continue;
                    }

                    var targetsRecords = new TargetViewModel
                    {
                        Quantity = int.Parse(cols[2]),
                    };
                    var targetId = await this.targetsService.CreateAsync(targetsRecords);

                    var discountRecords = new DiscountViewModel
                    {
                        Quantity = decimal.Parse(cols[3]),
                    };

                    var discountId = await this.discountsService.CreateAsync(discountRecords);

                    var medicineRecords = new MedicineViewModel
                    {
                        Name = cols[0].ToString(),
                        Price = decimal.Parse(cols[1]),
                        OfferId = offerId,
                        TargetId = targetId,
                        DiscountId = discountId,
                    };
                    var medicine = await this.medicinesService.CreateAsync(medicineRecords);
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);
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
