namespace Lekarna.Web.ViewModels.Pharmacies
{
    using System.Collections.Generic;

    public class AllPharmaciesViewModel
    {
        public AllPharmaciesViewModel()
        {
            this.Pharmacies = new HashSet<PharmacyViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PharmacyViewModel> Pharmacies { get; set; }
    }
}
