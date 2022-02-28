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

        public string GenerateStandardClassName(Type entityType)
        {
            return entityType.IsInterface && entityType.Name.StartsWith('I') ?
                    entityType.Name.Remove(0, 1) :
                    entityType.Name;
        }

        private string GenerateStandardExchangeName(Type entityType)
        {
            return $"{entityType.Namespace}.{GenerateStandardClassName(entityType)}";
        }
    }
}