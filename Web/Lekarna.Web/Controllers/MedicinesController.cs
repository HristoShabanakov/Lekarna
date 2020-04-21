namespace Lekarna.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Microsoft.AspNetCore.Mvc;

    public class MedicinesController : Controller
    {
        private readonly IMedicinesService medicinesService;
        private readonly IOffersService offersService;

        public MedicinesController(IMedicinesService medicinesService, IOffersService offersService)
        {
            this.medicinesService = medicinesService;
            this.offersService = offersService;
        }

        public IActionResult Create()
        {
            var offers = this.offersService.GetAll<OfferDropDownViewModel>();
            var viewModel = new MedicineViewModel
            {
                Offers = offers,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicineViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var medicineId = await this.medicinesService.CreateAsync(inputModel);

            if (medicineId == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.TempData["Notification"] = "Medicine was successfully registered!";
            return this.RedirectToAction(nameof(this.Create), new { id = medicineId });
        }
    }
}
