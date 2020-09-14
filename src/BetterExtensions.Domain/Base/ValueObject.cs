using System;
using System.Collections.Generic;
using System.Linq;
using BetterExtensions.Domain.Utils;

namespace BetterExtensions.Domain.Base
{
    [Serializable]
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        private int? _cachedHashCode;

        public override bool Equals(object obj)
        {
            if (!(obj is T valueObject))
                return false;

            if (this.GetUnproxiedType() != obj.GetUnproxiedType())
                return false;

            return EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            _cachedHashCode ??= GetHashCodeCore();
            return _cachedHashCode.Value;
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }

    [Serializable]
    public abstract class ValueObject
    {
        private int? _cachedHashCode;

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetUnproxiedType() != obj.GetUnproxiedType())
                return false;

            var valueObject = (ValueObject) obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            _cachedHashCode ??= GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return (current * 23) + (obj?.GetHashCode() ?? 0);
                    }
                });

            return _cachedHashCode.Value;
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}