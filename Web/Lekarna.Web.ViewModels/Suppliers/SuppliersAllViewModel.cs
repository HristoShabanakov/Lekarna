namespace Lekarna.Web.ViewModels.Suppliers
{
    using System.Collections.Generic;

    public class SuppliersAllViewModel
    {
        public SuppliersAllViewModel()
        {
            this.Suppliers = new HashSet<SupplierViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}
