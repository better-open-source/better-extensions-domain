using BetterExtensions.Domain.Utils;

namespace BetterExtensions.Domain.Base
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        protected Entity(int id)
            : this()
        {
            Id = id;
        }
        
        public int Id { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            if (this.GetUnproxiedType() != other.GetUnproxiedType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}