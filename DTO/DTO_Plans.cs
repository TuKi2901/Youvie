using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO
{
    public class DTO_Plans
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? PlaneName { get; set; }
        public int Price {  get; set; }
        public string? Duration { get; set; }
        public int Role { get; set; }
    }
}