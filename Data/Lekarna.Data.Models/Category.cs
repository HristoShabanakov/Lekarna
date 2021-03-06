﻿namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Category : BaseDeletableStringIdModel
    {
        public Category()
        {
            this.Offers = new HashSet<Offer>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
