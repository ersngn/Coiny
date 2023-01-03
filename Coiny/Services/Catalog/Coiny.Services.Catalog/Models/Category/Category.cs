using Coiny.Common.Models.Base;

namespace Coiny.Services.Catalog.Models.Category;

public class Category : EntityWithAudit
{
    public string? MainCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}