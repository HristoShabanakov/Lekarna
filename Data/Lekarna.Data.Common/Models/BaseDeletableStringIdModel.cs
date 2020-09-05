namespace Lekarna.Data.Common.Models
{
    using System;

    public abstract class BaseDeletableStringIdModel : BaseDeletableModel<string>
    {
        public BaseDeletableStringIdModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
