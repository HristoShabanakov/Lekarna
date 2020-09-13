namespace Lekarna.Web.ViewModels.Pharmacies
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static Lekarna.Common.GlobalConstants.Images;

    public class PharmacyViewModel : IMapFrom<Pharmacy>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserUsername { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Address { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        public IFormFile NewImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pharmacy, PharmacyViewModel>()
                .ForMember(
                    x => x.ImageUrl,
                    opt => opt.MapFrom(x => x.ImageUrl ?? LogoPath));
        }
    }
}
