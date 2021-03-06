﻿namespace Lekarna.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels;
    using Lekarna.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ISuppliersService suppliersService;

        public HomeController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Suppliers = await this.suppliersService.GetAllAsync<IndexSupplierViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Chat()
        {
            return this.View();
        }

        public IActionResult ErrorView()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
