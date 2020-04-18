namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class Medicine : BaseDeletableModel<string>
    {
        public Medicine()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Target { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }
    }
}
