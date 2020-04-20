namespace Lekarna.Data.Models
{
    using Lekarna.Data.Common.Models;

    public class OrderStatus : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
