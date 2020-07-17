using AutoMapper;

namespace TEP.Application.Common.Mappings
{
    public interface IMapTo<T>
    {
        public void MappingTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
