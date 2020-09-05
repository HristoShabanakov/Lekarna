namespace Lekarna.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class OrderItem : BaseDeletableStringIdModel
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string MedicineId { get; set; }

        public Medicine Medicine { get; set; }

        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
