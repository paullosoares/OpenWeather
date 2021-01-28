using System;
using FluentValidation.Results;

namespace Weather.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool Isvalid()
        {
            throw new NotImplementedException();
        }
    }
}
