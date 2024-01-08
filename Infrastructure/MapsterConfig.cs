using Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services) 
        {
            //TypeAdapterConfig<UserDB, User>
            //    .NewConfig()
            //    .Map(dest => dest.Id, src => src.Id.ToString())
            //    .Map(dest => dest.Name, src => src.Name)
            //    .Map(dest => dest.LastName, src => src.LastName)
            //    .Map(dest => dest.Identification, src => src.Identification)
            //    .Map(dest => dest.Email, src => src.Email)
            //    .Map(dest => dest.Phone, src => src.Phone)
            //    .Map(dest => dest.State, src => src.State);
            //    //.TwoWays();

            //TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
