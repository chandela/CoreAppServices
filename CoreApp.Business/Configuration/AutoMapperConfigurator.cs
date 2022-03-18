using AutoMapper;
using CoreApp.Models.Base;
using CoreApp.Models.CoreModels.BaseEntities;
using CoreApp.Models.CoreModels.SharedEntities;
using CoreApp.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Business.Configuration
{
    internal class AutoMapperConfigurator
    {
        internal static void CreateMaps(Action<IMapperConfigurationExpression> intializeUxMapper)
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<ObjectStateModel, ObjectState>().ReverseMap();
                mapper.CreateMap<EntityModel, BaseEntity>().ReverseMap();
                mapper.CreateMap<AddressModel, Address>().ReverseMap();
                mapper.CreateMap<UserModel, User>().ReverseMap();

                if (intializeUxMapper != null)
                    intializeUxMapper(mapper);
            });
        }
    }
}
