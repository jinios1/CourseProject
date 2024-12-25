using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Back.Models
{
    public class ItemCard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}