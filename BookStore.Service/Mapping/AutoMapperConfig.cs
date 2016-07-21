using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Service.Mapping
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public void Execute(params string[] assemblies)
        {
            Configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));
                    var types = new List<Type>();
                    foreach (var assembly in assemblies.Select(a => Assembly.Load(a)))
                    {
                        types.AddRange(assembly.GetExportedTypes());
                    }
                    LoadStandardMappings(types, cfg);
                    LoadReverseMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                }); 
         }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);

            }
        }

        private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}
