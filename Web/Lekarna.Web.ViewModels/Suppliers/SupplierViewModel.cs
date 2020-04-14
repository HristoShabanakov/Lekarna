﻿namespace Lekarna.Web.ViewModels.Suppliers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class SupplierViewModel : IMapFrom<Supplier>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public int OffersCount { get; set; }

        public int PagesCount { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public IFormFile NewImage { get; set; }

        public string Url => $"{this.Name.Replace(' ', '-')}";

        public IEnumerable<SuppliersOffersViewModel> Offers { get; set; }
    }
}
