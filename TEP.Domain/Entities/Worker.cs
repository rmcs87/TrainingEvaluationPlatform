using TEP.Domain.Common;

namespace TEP.Domain.Entities
{
    public abstract class Worker : AuditableEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Registry { get; private set; }
    }
}
