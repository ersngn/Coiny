namespace Coiny.Services.Catalog.Dtos.Category;

public class MainCategoryUpdateDto
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public bool IsActive { get; set; }
}