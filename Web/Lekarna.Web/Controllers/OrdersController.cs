namespace Lekarna.Web.Controllers
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data;
    using Lekarna.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMedicinesService medicinesService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(
            IMedicinesService medicinesService,
            IOrdersService ordersService,
            UserManager<ApplicationUser> userManager)
        {
            this.medicinesService = medicinesService;
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            // user.PharmacyId = inputModel.PharmacyId;
            var order = this.ordersService.CreateOrder(inputModel, user);

            if (order == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            var viewModel = this.ordersService.GetAll<CartViewModel>();

            var allOrders = new AllOrdersViewModel
            {
                Orders = viewModel,
            };

            return this.View(allOrders);
        }
    }
}
