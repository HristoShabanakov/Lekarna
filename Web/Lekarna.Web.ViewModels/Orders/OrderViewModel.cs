﻿namespace Lekarna.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IMapFrom<Medicine>
    {
        public string Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }

        public string PharmacyId { get; set; }

        public virtual PharamacyOrderViewModel Pharmacy { get; set; }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public string MedicineId { get; set; }

        public string Medicine { get; set; }

        public int StatusId { get; set; }

        public OrderStatusViewModel Status { get; set; }
    }
}
