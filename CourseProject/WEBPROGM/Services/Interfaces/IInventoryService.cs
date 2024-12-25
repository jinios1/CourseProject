using Back.Models;

namespace Back.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<ItemCard>> GetAllAsync();
        Task<ItemCard?> GetByIdAsync(string id);
        Task<ItemCard> CreateAsync(ItemCard item);
        Task<bool> UpdateAsync(string id, ItemCard updated);
        Task<bool> DeleteAsync(string id);
    }
}
