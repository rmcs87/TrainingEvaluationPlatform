using AutoMapper;

namespace TEP.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        public void MappingFrom(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
