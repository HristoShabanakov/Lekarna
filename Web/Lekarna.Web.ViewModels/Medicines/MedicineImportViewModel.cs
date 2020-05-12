namespace Lekarna.Web.ViewModels.Medicines
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MedicineImportViewModel
    {
        [Required]
        public IFormFile Data { get; set; }
    }
}
