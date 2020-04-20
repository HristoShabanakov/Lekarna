namespace Lekarna.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    public class AllOffersViewModel
    {
        public AllOffersViewModel()
        {
            this.Offers = new HashSet<OfferViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string Id { get; set; }

        public string OfferId { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
