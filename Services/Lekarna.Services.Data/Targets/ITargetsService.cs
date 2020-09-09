namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

<<<<<<< HEAD
    public interface ITargetsService
=======
    using Lekarna.Services.Data.Common;

    public interface ITargetsService : IService
>>>>>>> 214664bdd1a794b64e54961c0802de472cbe7488
    {
        Task<string> CreateAsync(int quantity);
    }
}
