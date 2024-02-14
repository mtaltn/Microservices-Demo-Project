using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RedLine.Services.Catalog.Models;

public class BaseModels
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }
}
