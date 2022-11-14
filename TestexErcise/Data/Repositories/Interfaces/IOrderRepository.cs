using TestExercise.Models;

namespace TestExercise.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public IQueryable<Order> Orders { get; }
        public Task<bool> AddOrderAsync(Order model);
        public Task<bool> UpdateOrderAsync(Order model);
        public Task<bool> RemoveOrderAsync(int id);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<bool> AddOrderItemAsync(OrderItem model);
        public Task<bool> RemoveOrderItemAsync(int id);
        public Task<OrderItem> GetOrderItemByIdAsync(int id);
    }
}
