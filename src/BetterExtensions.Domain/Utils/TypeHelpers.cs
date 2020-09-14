using System;

namespace BetterExtensions.Domain.Utils
{
    internal static class TypeHelpers
    {
        internal static Type GetUnproxiedType(this object obj)
        {
            const string efCoreProxyPrefix = "Castle.Proxies.";

            var type = obj.GetType();
            return type.ToString().Contains(efCoreProxyPrefix) 
                ? type.BaseType 
                : type;
        }
    }
}