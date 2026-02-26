namespace Auth.Application.Services.Interface
{
    public interface IMapper
    {
        TDestination  Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class;
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class;
    }
}
