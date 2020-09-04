﻿namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Pharmacy : BaseDeletableModel<string>
    {
        public Pharmacy()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Address { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
