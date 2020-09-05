namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class OrderItem : BaseDeletableStringIdModel
    {

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
