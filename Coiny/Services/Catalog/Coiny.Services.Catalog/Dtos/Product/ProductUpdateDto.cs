namespace Coiny.Services.Catalog.Dtos.Product;

public class ProductUpdateDto
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string? Description { get; set; }
    public string? SubDescription { get; set; }
    public string? SellerId { get; set; }
    public decimal Stock { get; set; }
    public string? CategoryId { get; set; }
}