﻿namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Common.Models;

    public class Discount : BaseDeletableStringIdModel
    {
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Quantity { get; set; }
    }
}
