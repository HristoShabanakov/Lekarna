namespace Lekarna.Web.ViewModels.Suppliers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static Lekarna.Common.GlobalConstants.Images;

    public class SupplierViewModel : IMapFrom<Supplier>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Supplier, SupplierViewModel>()
                .ForMember(
                    x => x.ImageUrl,
                    opt => opt.MapFrom(x => x.ImageUrl ?? LogoPath));
        }
    }
}
