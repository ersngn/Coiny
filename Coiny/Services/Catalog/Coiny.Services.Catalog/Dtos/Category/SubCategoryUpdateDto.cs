namespace Coiny.Services.Catalog.Dtos.Category;

public class SubCategoryUpdateDto
{
    public string Id { get; set; }
    public string? MainCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}