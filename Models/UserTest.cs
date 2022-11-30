using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample1.Models
{
    public class UserTest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        public string? uid { get; set; }

        public string fname { get; set; } = null!;

        public string mname { get; set; } = null!;

        public string lname { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get;  set; }
    }
}
