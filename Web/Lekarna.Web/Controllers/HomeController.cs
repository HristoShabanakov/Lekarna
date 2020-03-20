namespace Lekarna.Web.Controllers
{
    using System.Diagnostics;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels;
    using Lekarna.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IOffersService offersService;

        public HomeController(IOffersService offersService)
        {
            this.offersService = offersService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Offers = this.offersService.GetAll<IndexOfferViewModel>(),
            };
            return this.View(viewModel);
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
