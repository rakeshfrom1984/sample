using Motormechs.Business.Model;
using Motormechs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motormechs.API
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            #region Login
            AutoMapper.Mapper.CreateMap<Login, User>();
            AutoMapper.Mapper.CreateMap< User,Login>();
            #endregion

            #region User
            AutoMapper.Mapper.CreateMap<User, Users>();
            AutoMapper.Mapper.CreateMap<Users, User>();
            #endregion

            #region Vehicle
            AutoMapper.Mapper.CreateMap<Vehicle, Vehicles>();
            AutoMapper.Mapper.CreateMap<Vehicles, Vehicle>();
            #endregion

            #region Service
            AutoMapper.Mapper.CreateMap<Services, Service>();
            AutoMapper.Mapper.CreateMap<Service, Services>();
            #endregion

        }
    }
}