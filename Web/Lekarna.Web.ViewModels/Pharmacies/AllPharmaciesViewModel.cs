namespace Lekarna.Web.ViewModels.Pharmacies
{
    using System.Collections.Generic;

    public class AllPharmaciesViewModel
    {
        public IEnumerable<PharmacyViewModel> Pharmacies { get; set; }
    }
}
