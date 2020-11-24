namespace Lekarna.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OfferEditViewModel : IMapTo<Offer>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        [Required]
        [Display(Name = "Expiration Date (mm/dd/yyyy)")]
        public DateTime ExpirationDate { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Offer, OfferEditViewModel>()
                 .ForMember(x => x.ExpirationDate, opt => opt.MapFrom(y => y.ExpirationDate.ToLocalTime()));
        }
    }
}
