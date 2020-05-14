namespace Lekarna.Web.ViewModels.Medicines
{
    using System.Collections.Generic;

    public class AllRecordsViewModel
    {
        public AllRecordsViewModel()
        {
            this.Records = new HashSet<Record>();
            this.Targets = new HashSet<Target>();
        }

        public IEnumerable<Record> Records { get; set; }

        public IEnumerable<Target> Targets { get; set; }
    }
}
