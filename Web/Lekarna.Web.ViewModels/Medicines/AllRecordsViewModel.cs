namespace Lekarna.Web.ViewModels.Medicines
{
    using System.Collections.Generic;

    public class AllRecordsViewModel
    {
        public AllRecordsViewModel()
        {
            this.Records = new HashSet<Record>();
            this.Targets = new HashSet<Target>();
            this.Discounts = new HashSet<Discount>();
        }

        public IEnumerable<Record> Records { get; set; }

        public IEnumerable<Target> Targets { get; set; }

        public IEnumerable<Discount> Discounts { get; set; }
    }
}
