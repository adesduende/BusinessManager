using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; private set; }

        public Entity(Guid id)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            if(left is null || right is null)
            {
                return false;
            }
            return left.Equals(right);
        }
        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }
        public bool Equals(Entity? other)
        {
            if(other is null)
            {
                return false;
            }
            if(other.Id == Guid.Empty || Id == Guid.Empty)
            {
                return false;
            }
            return other.Id == Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Entity entity)
            {
                return Equals(entity);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
