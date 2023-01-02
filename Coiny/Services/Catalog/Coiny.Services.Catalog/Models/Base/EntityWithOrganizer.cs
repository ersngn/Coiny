using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coiny.Services.Catalog.Models.Base;

public class EntityWithOrganizer : EntityWithAudit
{
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; }
    public string? CreatedUserId { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedUserId { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUserId { get; set; }
}