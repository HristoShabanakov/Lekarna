namespace Lekarna.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OrderCreateViewModel : IMapFrom<Offer>
    {
        public string OfferId { get; set; }

        public string PharmacyId { get; set; }
    }
}
