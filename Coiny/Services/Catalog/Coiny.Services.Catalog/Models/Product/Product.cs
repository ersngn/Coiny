using Coiny.Services.Catalog.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coiny.Services.Catalog.Models.Product;

public class Product : EntityWithOrganizer
{
    public string? Name { get; set; }
    public string? Title { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal DiscountedPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? SubDescription { get; set; }
    public string? SellerId { get; set; }
    public string? SellerName { get; set; }
    [BsonRepresentation(BsonType.Decimal128)] 
    public decimal Stock { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CategoryId { get; set; }
    [BsonIgnore]
    public Category.Category? Category { get; set; }

}