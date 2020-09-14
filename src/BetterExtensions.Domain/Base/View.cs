using System;
using System.Collections.Generic;
using System.Linq;
using BetterExtensions.Domain.Utils;

namespace BetterExtensions.Domain.Base
{
    [Serializable]
    public abstract class View
    {
        private int? _cachedHashCode;

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetUnproxiedType() != obj.GetUnproxiedType())
                return false;

            var valueObject = (View) obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            _cachedHashCode ??= GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });

            return _cachedHashCode.Value;
        }

        public static bool operator ==(View a, View b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(View a, View b)
        {
            return !(a == b);
        }
    }
}