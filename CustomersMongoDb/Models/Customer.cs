using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CustomersMongoDb.Models
{
   
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string name { get; set; }

        [BsonElement("age")]
        [JsonProperty("age")]
        public int age { get; set; }

        [BsonElement("active")]
        [JsonProperty("active")]
        public bool active { get; set; }

        [BsonElement("__v")]
        [JsonProperty("__v")]
        public long __v { get; set; }

    }
}
