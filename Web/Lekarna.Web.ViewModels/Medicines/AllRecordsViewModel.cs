namespace Lekarna.Web.ViewModels.Medicines
{
    using System.Collections.Generic;

    public class AllRecordsViewModel
    {
        public AllRecordsViewModel()
        {
            this.Records = new HashSet<Record>();
        }
        public IEnumerable<Record> Records { get; set; }
    }
}
