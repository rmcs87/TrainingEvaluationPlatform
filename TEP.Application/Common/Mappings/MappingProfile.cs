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
                                     || i.GetGenericTypeDefinition() == typeof(IMapTo<>) ) ))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                InvokeMappings(instance, type, this);
            }
        }
        
        private static void InvokeMappings(Object instance, Type type, MappingProfile mappingProfile)
        {
            MethodInfo methodInfo = type.GetMethod("Mapping");
            if (type.GetMethod("Mapping") != null)
                methodInfo.Invoke(instance, new object[] { mappingProfile });
            else
            {
                type.GetInterface("IMapFrom`1")?.GetMethod("Mapping").Invoke(instance, new object[] { mappingProfile });
                type.GetInterface("IMapTo`1")?.GetMethod("Mapping").Invoke(instance, new object[] { mappingProfile });
            }
        }
    }
}
