using System;
using Template.Domain.Core.Interfaces;

namespace Template.Domain.Core.Entities
{
    public abstract class EntityBase : IDomainEntity, IHistorical
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name { get; set; }
    }
}
