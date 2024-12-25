using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Back.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Models.User> Users
            => _database.GetCollection<Models.User>("Users");

        public IMongoCollection<Models.ItemCard> Items
            => _database.GetCollection<Models.ItemCard>("Items");

        public IMongoCollection<Models.Order> Orders
            => _database.GetCollection<Models.Order>("Orders");

    }
}
