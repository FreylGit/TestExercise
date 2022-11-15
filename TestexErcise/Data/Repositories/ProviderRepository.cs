using TestexErcise.Data;
using TestExercise.Data.Repositories.Interfaces;
using TestExercise.Models;

namespace TestExercise.Data.Repositories
{
    public class ProviderRepository : Repository, IProviderRepository
    {
        private readonly ApplicationDbContext _context;
        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Provider> Providers => CheckConnectDatabase(_context) ? _context.Providers : null;

        public async Task<bool> AddAsync(Provider model)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    await _context.Providers.AddAsync(model);
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

        public Provider GetProviderById(int id)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                var model = _context.Providers.FirstOrDefault(p => p.Id == id);
                if (model == null)
                {
                    return null;
                }
                return model;
                }
                return null;
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
                if (CheckConnectDatabase(_context))
                {
                    if (_context.Providers.Remove(model) != null)
                    {
                        await _context.SaveChangesAsync();
                        return true;
                    }
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
                if (CheckConnectDatabase(_context))
                {
                    if (_context.Providers.Update(model) != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
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
