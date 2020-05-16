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
            var targetList = new List<Target>();
            var discountList = new List<Discount>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string[] headers = reader.ReadLine()
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var textReader = reader.ReadToEnd()
                    .Replace(" лв.", string.Empty).Replace("%", string.Empty).Replace("\"", string.Empty)
                    .Trim();
                var rows = textReader.Split("\r\n");
                int index = 0;
                for (int i = 0; i < rows.Length; i++)
                {
                    var cols = rows[i].Split(";");
                    string name = cols[0];
                    string price = cols[1];
                    string target = cols[2];
                    string discount = cols[3];

                    if (name.Contains("Total") && target.Any())
                    {
                        for (int j = 0; j < targetList.Count; j++)
                        {
                           if (targetList[j].TotalTarget.Length == 0)
                            {
                                targetList[j].TotalTarget = target;
                            }
                        }

                        index++;
                    }

                    if (name.Any() && price.Any() && target.Length == 0 && discount.Any())
                    {
                        targetList.Add(new Target
                        {
                            Id = index + 1,
                            Name = cols[0],
                            Price = cols[1],
                            TotalTarget = cols[2],
                            Discount = cols[3],
                        });
                    }

                    if (name.Any() && price.Any() && target.Any() && discount.Length == 0)
                    {
                        cols[3] = "0";
                    }

                    if (name.Contains("FORMULA") && discount.Any())
                    {
                        discountList.Add(new Discount
                        {
                            Name = cols[0],
                            Personal = decimal.Parse(cols[3]),
                        });
                    }

                    if (cols.Contains(string.Empty))
                    {
                        continue;
                    }

                    recordsList.Add(new Record
                    {
                        TargetId = ++index,
                        Name = cols[0].ToString(),
                        Price = decimal.Parse(cols[1]),
                        Target = int.Parse(cols[2]),
                        Discount = decimal.Parse(cols[3]),
                    });
                }

                // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                // {
                //    var records = csv.GetRecords<Record>().ToList();
                //    viewModel.Records = records;
                // }
            }

            viewModel.Records = recordsList;
            viewModel.Targets = targetList;
            viewModel.Discounts = discountList;
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
