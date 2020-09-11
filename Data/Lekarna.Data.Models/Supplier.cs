namespace Lekarna.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Supplier : BaseDeletableStringIdModel
    {
        public Supplier()
        {
            this.Offers = new HashSet<Offer>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
