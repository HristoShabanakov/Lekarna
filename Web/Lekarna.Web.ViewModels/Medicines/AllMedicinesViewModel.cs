namespace Lekarna.Web.ViewModels.Medicines
{
    using System.Collections.Generic;

    public class AllMedicinesViewModel
    {
        public AllMedicinesViewModel()
        {
            this.Medicines = new HashSet<MedicineViewModel>();
        }

        public IEnumerable<MedicineViewModel> Medicines { get; set; }
    }
}
