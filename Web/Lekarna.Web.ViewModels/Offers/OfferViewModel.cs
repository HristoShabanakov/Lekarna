namespace Lekarna.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Medicines;
    using Lekarna.Web.ViewModels.Pharmacies;

    public class OfferViewModel : IMapFrom<Offer>, IMapFrom<Medicine>, IMapFrom<Target>, IMapFrom<Pharmacy>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string PharmacyId { get; set; }

        public string CategoryId { get; set; }

        public string SupplierId { get; set; }

        public string CategoryCategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }

        public IEnumerable<MedicineViewModel> Medicines { get; set; }

        public IEnumerable<PharmacyViewModel> Pharmacies { get; set; }
    }
}
