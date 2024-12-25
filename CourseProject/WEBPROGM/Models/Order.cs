using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Back.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public List<string> ItemIds { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "New";
    }
}