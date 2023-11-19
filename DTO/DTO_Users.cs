using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO
{
    public class DTO_Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string? Country { get; set; }
        public DTO_Accounts? Account { get; set; }
        public DTO_Plans? Plan { get; set; }
        public List<string>? Collection {  get; set; }
    }
}
