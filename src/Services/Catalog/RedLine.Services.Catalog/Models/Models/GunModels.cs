using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RedLine.Services.Catalog.Models;

public class GunModels : BaseModels
{
    public string Picture { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }
    public FeatureModels FeatureModels { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedTime { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategotyId { get; set; }

    [BsonIgnore]
    public CategoryModels CategoryModels { get; set; }
}
