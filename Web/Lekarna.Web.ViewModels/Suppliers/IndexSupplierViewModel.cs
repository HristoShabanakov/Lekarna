namespace Lekarna.Web.ViewModels.Suppliers
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class IndexSupplierViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public IFormFile NewImage { get; set; }

        public int OffersCount { get; set; }

        public string Url => $"{this.Name.Replace(' ', '-')}";

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
