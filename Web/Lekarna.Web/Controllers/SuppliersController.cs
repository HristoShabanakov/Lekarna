namespace Lekarna.Web.Controllers
{
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public IActionResult ByCompany(string name)
        {
            var viewModel = this.suppliersService.GetByName<SupplierViewModel>(name);

            return this.View(viewModel);
        }
    }
}
