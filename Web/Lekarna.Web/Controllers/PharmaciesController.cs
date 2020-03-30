namespace Lekarna.Web.Controllers
{
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Pharmacies;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
    }
}
