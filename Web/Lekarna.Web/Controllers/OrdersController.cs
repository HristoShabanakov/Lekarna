namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var order = await this.ordersService
                .CreateOrderAsync(inputModel.OfferId, inputModel.PharmacyId);

            if (order == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            var viewModel = await this.ordersService.GetAllAsync<CartViewModel>();

            var allOrders = new AllOrdersViewModel
            {
                Orders = viewModel,
            };

            return this.View(allOrders);
        }
    }
}
