namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Medicines = new HashSet<Medicine>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
