namespace Lekarna.Services.Data
{
    using System.Collections.Generic;

    public interface IOffersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
