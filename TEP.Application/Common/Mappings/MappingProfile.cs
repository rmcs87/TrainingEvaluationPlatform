using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace TEP.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                   i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                                     || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                InvokeMappings(instance, type, this);
            }
        }

        private static void InvokeMappings(Object instance, Type type, MappingProfile mappingProfile)
        {
            if (type.GetMethod("MappingFrom") != null)
            {
                type.GetMethod("MappingFrom").Invoke(instance, new object[] { mappingProfile });
            }
            else
            {
                type.GetInterface("IMapFrom`1")?.GetMethod("MappingFrom").Invoke(instance, new object[] { mappingProfile });
            }
            if (type.GetMethod("MappingTo") != null)
            {
                type.GetMethod("MappingTo").Invoke(instance, new object[] { mappingProfile });
            }
            else
            {
                type.GetInterface("IMapTo`1")?.GetMethod("MappingTo").Invoke(instance, new object[] { mappingProfile });
            }
        }
    }
}
