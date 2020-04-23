namespace Lekarna.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class PharamacyOrderViewModel : IdentityUser
    {
        public PharamacyOrderViewModel()
        {
            this.Orders = new HashSet<OrderViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
