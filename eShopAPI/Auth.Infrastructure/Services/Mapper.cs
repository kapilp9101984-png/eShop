using Auth.Application.Services.Interface;
using Riok.Mapperly.Abstractions;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Auth.Infrastructure.Services
{
    [Mapper]
    public partial class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return default!;

            // Use JSON round-trip to map between source and destination types.
            // This is a simple generic approach that works for DTO <-> Entity mapping
            // without needing explicit member mappings.
            var json = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<TDestination>(json)!;
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return Enumerable.Empty<TDestination>();

            return source.Select(s => Map<TSource, TDestination>(s)).ToList();
        }
    }
}
