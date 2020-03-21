namespace Lekarna.Services.Data
{
    using System.Collections.Generic;

    public interface ISuppliersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
