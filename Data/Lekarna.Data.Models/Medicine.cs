﻿namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class Medicine : BaseDeletableStringIdModel
    {
        public Medicine()
        {
            this.OrdersItems = new HashSet<OrderItem>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string TargetId { get; set; }

        public Target Target { get; set; }

        public Discount Discount { get; set; }

        public string DiscountId { get; set; }

        public string OfferId { get; set; }

        public Offer Offer { get; set; }

        public ICollection<OrderItem> OrdersItems { get; set; }
    }
}
