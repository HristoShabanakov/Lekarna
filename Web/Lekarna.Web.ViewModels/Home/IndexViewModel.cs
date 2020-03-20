namespace Lekarna.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexSupplierViewModel> Suppliers { get; set; }
    }
}
