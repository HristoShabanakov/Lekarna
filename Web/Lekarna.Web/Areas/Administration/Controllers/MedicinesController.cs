namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CsvHelper;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Medicines;
    using Microsoft.AspNetCore.Mvc;

    public class MedicinesController : AdministrationController
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

        public IActionResult Import()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Records(MedicineImportViewModel inputModel)
        {
            var file = inputModel.Data;
            string fileExtension = Path.GetExtension(inputModel.Data.FileName);

            if (fileExtension != ".csv")
            {
                this.ViewBag.Message = "Please select the csv file with .csv extension";
                return this.View();
            }

            var viewModel = new AllRecordsViewModel();
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Record>().ToList();
                viewModel.Records = records;
            }

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
            return this.RedirectToAction("All", "Offers");
        }
    }
}
