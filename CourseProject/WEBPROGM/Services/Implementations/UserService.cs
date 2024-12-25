using Back.Data;
using Back.Models;
using Back.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Back.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(MongoDbContext context)
        {
            _usersCollection = context.Users;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var all = await _usersCollection.Find(_ => true).ToListAsync();
            return all;
        }

        public async Task<User?> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();

            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<bool> UpdateUserAsync(string id, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);

            var update = Builders<User>.Update
                .Set(u => u.Email, updatedUser.Email)
                .Set(u => u.Password, updatedUser.Password)
                .Set(u => u.Name, updatedUser.Name);

            var result = await _usersCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var result = await _usersCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
