namespace Lekarna.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Pharmacy : BaseDeletableStringIdModel
    {
        public Pharmacy()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(30)]
        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
