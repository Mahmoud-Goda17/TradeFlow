
namespace Shared.Commen
{
    public abstract class AuditableEntity : Entity
    {
        public AuditableEntity() { }
        public AuditableEntity(Guid id) : base(id) { }
        public DateTimeOffset CreatedAtUtc { get; set; }


        public DateTimeOffset LastModifiedUtc { get; set; }


    }
}
