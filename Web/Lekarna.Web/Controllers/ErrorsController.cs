namespace Lekarna.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorsController : Controller
    {
        public new IActionResult NotFound()
        {
            return this.View();
        }

        public IActionResult Forbidden()
        {
            return this.View();
        }

        public new IActionResult BadRequest()
        {
            return this.View();
        }
    }
}
