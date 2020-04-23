namespace Lekarna.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class AllOrdersViewModel
    {
        public AllOrdersViewModel()
        {
            this.Orders = new HashSet<CartViewModel>();
        }

        public IEnumerable<CartViewModel> Orders { get; set; }
    }
}
