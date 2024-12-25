using Back.Data;
using Back.Models;
using Back.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Back.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(MongoDbContext context)
        {
            _orders = context.Orders;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(string id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
            return await _orders.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.Id = ObjectId.GenerateNewId().ToString();
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<bool> UpdateAsync(string id, Order updated)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);

            var updateDef = Builders<Order>.Update
                .Set(o => o.UserId, updated.UserId)
                .Set(o => o.ItemIds, updated.ItemIds)
                .Set(o => o.TotalPrice, updated.TotalPrice)
                .Set(o => o.Status, updated.Status);

            var result = await _orders.UpdateOneAsync(filter, updateDef);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
            var result = await _orders.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
