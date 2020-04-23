namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using Lekarna.Data.Common.Models;

    public class OrderItem : BaseDeletableModel<string>
    {
        public OrderItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

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
