namespace Lekarna.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Pharmacies;

    public class OfferViewModel : IMapFrom<Offer>, IMapFrom<Medicine>, IMapFrom<Target>, IMapFrom<Pharmacy>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string PharmacyId { get; set; }

        public string CategoryId { get; set; }

        public string SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string CategoryCategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }

        public IEnumerable<MedicineViewModel> Medicines { get; set; }

        public IEnumerable<PharmacyViewModel> Pharmacies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Offer, OfferViewModel>()
                 .ForMember(x => x.ExpirationDate, opt => opt.MapFrom(y => y.ExpirationDate.ToLocalTime()));
        }
    }
}
