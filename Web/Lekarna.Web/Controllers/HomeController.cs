namespace Lekarna.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Lekarna.Data;
    using Lekarna.Web.ViewModels;
    using Lekarna.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var offers = this.dbContext.Offers.Select(x => new IndexOfferViewModel
            {
                 Name = x.Name,
                 ImageUrl = x.ImageUrl,
                 CreatedOn = x.CreatedOn,
            }).ToList();

            viewModel.Offers = offers;

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
