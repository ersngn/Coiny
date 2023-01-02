using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coiny.Services.Catalog.Models.Base;

public class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}