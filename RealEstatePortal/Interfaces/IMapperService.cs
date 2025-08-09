
namespace RealEstatePortal.Services
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
    }
}