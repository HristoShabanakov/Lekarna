namespace Lekarna.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        public IActionResult ByCompany(string name)
        {
            return this.View();
        }
    }
}
