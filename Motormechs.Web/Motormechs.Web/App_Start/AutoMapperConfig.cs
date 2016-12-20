using Motormechs.Business.Model;
using Motormechs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motormechs.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            #region Vehicles
            AutoMapper.Mapper.CreateMap<VehicleViewModel, Vehicles>();
            AutoMapper.Mapper.CreateMap<Vehicles, VehicleViewModel>();
            #endregion

            #region Users
            AutoMapper.Mapper.CreateMap<ManageUserViewModel, Users>();
            AutoMapper.Mapper.CreateMap<Users, ManageUserViewModel>();
            #endregion

            #region ChangePasswordModel
            AutoMapper.Mapper.CreateMap<ChangePasswordViewModel, ChangePasswordModel>();
            AutoMapper.Mapper.CreateMap<ChangePasswordModel, ChangePasswordViewModel>();
            #endregion

            #region NewPasswordModel
            AutoMapper.Mapper.CreateMap<NewPasswordViewModel, NewPasswordModel>();
            AutoMapper.Mapper.CreateMap<NewPasswordModel, NewPasswordViewModel>();
            #endregion
            

        }
    }
}