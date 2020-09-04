namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;

    public class TargetsService : ITargetsService
    {
        private readonly IDeletableEntityRepository<Target> targetsRepository;

        public TargetsService(IDeletableEntityRepository<Target> targetsRepository)
        {
            this.targetsRepository = targetsRepository;
        }

        public async Task<string> CreateAsync(int quantity)
        {
            var target = new Target
            {
                Quantity = quantity,
            };

            await this.targetsRepository.AddAsync(target);
            await this.targetsRepository.SaveChangesAsync();
            return target.Id;
        }
    }
}
