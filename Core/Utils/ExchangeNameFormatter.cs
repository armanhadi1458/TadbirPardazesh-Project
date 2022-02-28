using MassTransit.Topology;
using System;

namespace Core.Utils
{
    public class ExchangeNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            var entityType = typeof(T);
            return GenerateStandardExchangeName(entityType);
        }

        private string GenerateStandardExchangeName(Type entityType)
        {
            return entityType.IsInterface && entityType.Name.StartsWith('I') ?
                $"{entityType.Namespace}.{entityType.Name.Remove(0, 1)}" :
                entityType.FullName;
        }
    }
}