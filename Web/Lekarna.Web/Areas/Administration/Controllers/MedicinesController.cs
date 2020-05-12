namespace Lekarna.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
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
                this.TempData["Notification"] = "Please select the csv file with .csv extension !";
                return this.View();
            }

            var viewModel = new AllRecordsViewModel();
            var recordsList = new List<Record>();
            string line;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string[] headers = reader.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                while ((line = reader.ReadLine()) != null)
                {
                    string[] rows = line.Split(';');

                    string name = rows[0];
                    string price = rows[1];
                    string target = rows[2];
                    string discount = rows[3];

                    if (name.StartsWith("\"") && name.EndsWith("\""))
                    {
                        rows[0] = name.Replace("\"", " ").Trim();
                    }

                    if (price.Contains("лв."))
                    {
                        rows[1] = price.Replace("лв.", " ").Trim();
                    }

                    if (target.Length == 0)
                    {
                        rows[2] = "0";
                    }

                    if (discount.Contains("%"))
                    {
                        rows[3] = discount.Replace("%", " ").Trim();
                    }

                    if (rows.Contains(string.Empty))
                    {
                        continue;
                    }

                    if (rows.Length == 4)
                    {
                        recordsList.Add(new Record
                        {
                            Name = rows[0].ToString(),
                            Price = decimal.Parse(rows[1]),
                            Target = int.Parse(rows[2]),
                            Discount = decimal.Parse(rows[3]),
                        });
                    }
                }

                viewModel.Records = recordsList;

                // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                // {
                //    var records = csv.GetRecords<Record>().ToList();
                //    viewModel.Records = records;
                // }
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
