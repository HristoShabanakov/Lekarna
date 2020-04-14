// ReSharper disable VirtualMemberCallInConstructor
namespace Lekarna.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Lekarna.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Orders = new HashSet<Order>();
            this.Pharmacies = new HashSet<Pharmacy>();
            this.Categories = new HashSet<Category>();
            this.Suppliers = new HashSet<Supplier>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Pharmacy> Pharmacies { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public string PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
