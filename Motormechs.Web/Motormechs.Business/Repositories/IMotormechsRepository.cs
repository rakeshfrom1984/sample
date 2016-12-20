using Motormechs.Business.Model;
using Motormechs.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Repositories
{
    public interface IMotormechsRepository : IDisposable
    {
        Users CheckLogin(Login login);
        Users CreateUser(Users user);
        Role CreateRole(Role role);
        Role UpdateRole(Role role);
        //bool CheckToken(int userId , string token);
        Vehicles AddVehicle(Vehicles vehicles);
        Vehicles getVehicleDetail(int userId);
        Vehicles getVehicleDetailById(int Id, int userId);
        Vehicles DeleteVehicle(int Id, int userId);
        Users GetUserDetail(int login);
        Users UpdateMembershipForForgetPassword(string Email, Guid Fpc, DateTime Fpd, bool Fps);
        Users UpdateUserDetail(Users login);
        ChangePasswordModel ChangePassword(ChangePasswordModel pass);
        Services GetServices();
        Services AddServices(NewServiceModel model);
        UserServiceList GetUserServices(int userId);
        UserServiceDetailList GetUserServiceDetail(int OrderId);
        List<PasswordStatus> GetForgetPasswordStatus(Guid Fpc);
        NewPasswordModel NewPassword(NewPasswordModel pass);
    }
}
