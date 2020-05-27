namespace Lekarna.Web.ViewModels.Medicines
{
    using System.Collections.Generic;

    public class AllRecordsViewModel
    {
        public AllRecordsViewModel()
        {
            this.Records = new HashSet<MedicineRecords>();
        }

        public IEnumerable<MedicineRecords> Records { get; set; }
    }
}
