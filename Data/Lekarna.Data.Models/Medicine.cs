namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class Medicine : BaseDeletableStringIdModel
    {
        public Medicine()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string TargetId { get; set; }

        public virtual Target Target { get; set; }

        public virtual Discount Discount { get; set; }

        public string DiscountId { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
