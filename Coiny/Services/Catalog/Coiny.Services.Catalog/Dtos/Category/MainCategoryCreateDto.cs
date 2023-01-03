using Coiny.Common.Dtos.Base;

namespace Coiny.Services.Catalog.Dtos.Category;

public class MainCategoryCreateDto : IDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}