namespace Lekarna.Web.ViewModels.Home
{
    using System;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class IndexOfferViewModel : IMapFrom<Offer>
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"{this.Name.Replace(' ', '-')}";
    }
}
