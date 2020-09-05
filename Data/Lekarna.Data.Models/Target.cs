namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Target : BaseDeletableStringIdModel
    {
        [Required]
        public int Quantity { get; set; }
    }
}
