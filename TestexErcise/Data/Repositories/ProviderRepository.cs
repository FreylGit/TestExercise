using AutoMapper;
using TestexErcise.Data;
using TestExercise.Data.Repositories.Interfaces;
using TestExercise.Models;

namespace TestExercise.Data.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDbContext _context;
        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Provider> Providers => _context.Providers;

        public async Task<bool> AddAsync(Provider model)
        {
            try
            {
                await _context.Providers.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Provider GetProviderById(int id)
        {
            try
            {
                var model = _context.Providers.FirstOrDefault(p => p.Id == id);
                if (model == null)
                {
                    return null;
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Provider model)
        {
            try
            {
                if (_context.Providers.Remove(model) != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Provider model)
        {
            try
            {
                if (_context.Providers.Update(model) != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
