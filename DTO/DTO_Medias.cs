using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO
{
    public class DTO_Medias
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        //public string? MediaName { get; set; }
        public string? Country { get; set; }
        //public DateOnly ReleaseDate { get; set; }
        public int CountEpisode { get; set; }
        //public List<Category>? ListCategory { get; set; }
        //public List<string>? ListEpisode {  get; set; }
        public int RoleMedia {  get; set; }
        public string? Decription { get; set; }
        public string? Image {  get; set; }
    }
}
