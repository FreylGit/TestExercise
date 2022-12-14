using Microsoft.EntityFrameworkCore;
using TestexErcise.Data;
using TestExercise.Data.Repositories.Interfaces;
using TestExercise.Models;

namespace TestExercise.Data.Repositories
{
    public class OrderRepository : Repository,IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => CheckConnectDatabase(_context) ? _context.Orders.Include(o => o.Items).Include(o => o.Provider) :null;

        public async Task<bool> AddOrderAsync(Order model)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    if (!(Convert.ToUInt32(model.Number) == model.ProviderId))
                    {
                        await _context.Orders.AddAsync(model);
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

        public async Task<bool> AddOrderItemAsync(OrderItem model)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    model.Id = 0;
                    var order = _context.Orders.FirstOrDefault(o => o.Id == model.OrderId);
                    if (order.Number != model.Name)
                    {
                        await _context.OrderItems.AddAsync(model);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                   
                return false;

            }
            catch (Exception ex) { return false; }

        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    var model = await Orders.FirstOrDefaultAsync(o => o.Id == id);
                    if (model.Items == null)
                    {
                        model.Items = new List<OrderItem>();
                    }
                    return model;
                }
                return null;

            }
            catch (Exception ex) { return null; }
        }

        public Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    var model = _context.OrderItems.FirstOrDefaultAsync(i => i.Id == id);
                    if (model != null)
                    {
                        return model;
                    }
                } 
                return null;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<bool> RemoveOrderAsync(int id)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    var model = _context.Orders.FirstOrDefault(o => o.Id == id);
                    if (_context.Orders.Remove(model) != null)
                    {
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                   
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<bool> RemoveOrderItemAsync(int id)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    var model = _context.OrderItems.FirstOrDefault(i => i.Id == id);
                    if (model != null)
                    {
                        _context.OrderItems.Remove(model);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                   
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<bool> UpdateOrderAsync(Order model)
        {
            try
            {
                if (CheckConnectDatabase(_context))
                {
                    _context.Orders.Update(model);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e) { return false; }
        }
    }
}
