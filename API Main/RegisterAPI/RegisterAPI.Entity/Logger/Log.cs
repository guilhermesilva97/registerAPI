using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RegisterAPI.Entity.Logger
{
    public class Log
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("Method")]
        public string? Method { get; set; }

        [BsonElement("Object")]
        public string? Object { get; set; }

        [BsonElement("Error")]
        public string Error { get; set; }
    }
}
