namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Targets;

    public class TargetsService : ITargetsService
    {
        private readonly IDeletableEntityRepository<Target> targetsRepository;

        public TargetsService(IDeletableEntityRepository<Target> targetsRepository)
        {
            this.targetsRepository = targetsRepository;
        }

        public async Task<string> CreateAsync(TargetViewModel inputModel)
        {
            var target = new Target
            {
                Quantity = inputModel.Quantity,
            };

            await this.targetsRepository.AddAsync(target);
            await this.targetsRepository.SaveChangesAsync();
            return target.Id;
        }
    }
}
