using Coiny.Common.Dtos.Base;

namespace Coiny.Services.Catalog.Dtos.Category;

public class CategoryDto : IDto
{
    public string Id { get; set; }
    public string? MainCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}