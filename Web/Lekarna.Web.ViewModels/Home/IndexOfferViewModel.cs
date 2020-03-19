namespace Lekarna.Web.ViewModels.Home
{
    using System;

    public class IndexOfferViewModel
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"{this.Name.Replace(' ', '-')}";
    }
}
