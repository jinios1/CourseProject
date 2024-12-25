using Back.Data;
using Back.Models;
using Back.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Back.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IMongoCollection<ItemCard> _items;

        public InventoryService(MongoDbContext context)
        {
            _items = context.Items;
        }

        public async Task<IEnumerable<ItemCard>> GetAllAsync()
        {
            return await _items.Find(_ => true).ToListAsync();
        }

        public async Task<ItemCard?> GetByIdAsync(string id)
        {
            var filter = Builders<ItemCard>.Filter.Eq(x => x.Id, id);
            return await _items.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ItemCard> CreateAsync(ItemCard item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await _items.InsertOneAsync(item);
            return item;
        }

        public async Task<bool> UpdateAsync(string id, ItemCard updated)
        {
            var filter = Builders<ItemCard>.Filter.Eq(x => x.Id, id);

            var updateDef = Builders<ItemCard>.Update
                .Set(x => x.Name, updated.Name)
                .Set(x => x.Quantity, updated.Quantity);

            var result = await _items.UpdateOneAsync(filter, updateDef);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<ItemCard>.Filter.Eq(x => x.Id, id);
            var result = await _items.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
