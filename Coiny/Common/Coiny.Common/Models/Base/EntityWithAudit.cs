namespace Coiny.Common.Models.Base;

public class EntityWithAudit : Entity
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}