﻿namespace Lekarna.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexOfferViewModel> Offers { get; set; }
    }
}