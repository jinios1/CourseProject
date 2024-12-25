using Back.Models;

namespace Back.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(string id);
        Task<Order> CreateAsync(Order order);
        Task<bool> UpdateAsync(string id, Order updated);
        Task<bool> DeleteAsync(string id);
    }
}
