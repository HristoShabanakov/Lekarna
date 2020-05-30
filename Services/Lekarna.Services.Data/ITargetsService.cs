namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Web.ViewModels.Targets;

    public interface ITargetsService
    {
        Task<string> CreateAsync(TargetViewModel inputModel);
    }
}
