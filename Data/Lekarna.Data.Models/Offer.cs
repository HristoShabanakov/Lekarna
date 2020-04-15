namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Medicine { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Target { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Discount { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
