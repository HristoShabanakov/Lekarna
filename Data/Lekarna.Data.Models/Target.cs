namespace Lekarna.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Lekarna.Data.Common.Models;

    public class Target : BaseDeletableModel<string>
    {
        public Target()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public int Quantity { get; set; }
    }
}
