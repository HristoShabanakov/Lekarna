namespace Lekarna.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Medicines;

    public class OfferViewModel : IMapFrom<Offer>, IMapFrom<Medicine>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string MedicineName { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public string SupplierId { get; set; }

        public string OfferId { get; set; }

        public string CategoryCategoryName { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SupplierDropDownViewModel> Suppliers { get; set; }

        public IEnumerable<MedicineViewModel> Medicines { get; set; }
    }
}
