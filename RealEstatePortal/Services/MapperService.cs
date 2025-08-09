using AutoMapper;
using AutoMapper.QueryableExtensions;
using RealEstatePortal.Profiles;
using RealEstatePortal.Services;

public class MapperService : IMapperService
{
    private readonly IMapper _mapper;
    private readonly AutoMapper.IConfigurationProvider _configuration;
    public MapperService(ILoggerFactory loggerFactory)
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }
        
    public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source) =>
        source.ProjectTo<TDestination>(_configuration);
}