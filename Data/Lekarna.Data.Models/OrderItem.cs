namespace Lekarna.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        public string MedicineId { get; set; }

        public Medicine Medicine { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
