using System.Text.Json;
using AutoMapper;
using RealEstatePortal.Models;

namespace RealEstatePortal.Profiles
{
    public class JsonToListResolver : IValueResolver<Property, object, List<string>>
    {
        public List<string> Resolve(Property source, object destination, List<string> destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrls))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(source.ImageUrls) ?? new List<string>();
            }
            catch (JsonException)
            {
                return new List<string>();
            }
        }
    }
}