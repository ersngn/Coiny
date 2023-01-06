using Coiny.Common.Dtos.Base;

namespace Coiny.Services.Catalog.Dtos.Category;

public class CategoryTreeDto : IDto
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public List<CategoryDto>? SubCategories { get; set; }
}