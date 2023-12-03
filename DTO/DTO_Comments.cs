using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? NameUser { get; set; }
        public string? IdMedia { get; set; }
    }
}
