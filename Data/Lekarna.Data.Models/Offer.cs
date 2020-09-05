namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Offer : BaseDeletableStringIdModel
    {
        public Offer()
        {
            this.Medicines = new HashSet<Medicine>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Medicine> Medicines { get; set; }
    }
}
