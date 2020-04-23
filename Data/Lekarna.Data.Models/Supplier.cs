namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Supplier : BaseDeletableModel<string>
    {
        public Supplier()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Offers = new HashSet<Offer>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Address { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
