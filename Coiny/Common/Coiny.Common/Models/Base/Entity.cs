using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coiny.Common.Models.Base;

public class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}