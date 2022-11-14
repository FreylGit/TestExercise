using TestExercise.Models;

namespace TestExercise.Data.Repositories.Interfaces
{
    public interface IProviderRepository
    {
        IQueryable<Provider> Providers { get; }
        public Task<bool> AddAsync(Provider model);
        public Task<bool> RemoveAsync(Provider model);
        public Task<bool> UpdateAsync(Provider model);
        public Provider GetProviderById(int id);
    }
}
