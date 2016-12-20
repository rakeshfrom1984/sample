using Motormechs.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motormechs.Data;
using Motormechs.Data.Entity;
using System.Data.Entity;
using System.Transactions;
using System.Collections;
using System.Data.SqlClient;

namespace Motormechs.Business.Repositories
{
    public class MotormechsRepository : IMotormechsRepository
    {
        #region Global Variable.............................

        MotorMechsEntities obj;

        #endregion

        #region Method
        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> user detail</returns>
        public Users GetUserDetail(int login)
        {
            User eUser = new User();
            Users bUser = new Users();
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.Id == login).FirstOrDefault();
            }
            if (bUser != null)
            {

                bUser = AutoMapper.Mapper.Map<Users>(eUser);
                bUser.Message = "User Exist";
                bUser.Status = true;

            }
            else  //user does not exist
            {

                bUser = AutoMapper.Mapper.Map<Users>(eUser);
                bUser.Message = "User does not exist";
                bUser.Status = false;
            }
            return bUser;
        }

        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> user detail</returns>
        public Users UpdateMembershipForForgetPassword(string Email, Guid Fpc, DateTime Fpd, bool Fps)
        {
            User eUser = new User();
            Users bUser = new Users();
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.Email == Email).FirstOrDefault();

                if (eUser != null)
                {
                    eUser.ForgetPasswordCode = Fpc;
                    eUser.ForgetPasswordDate = Fpd;
                    eUser.ForgetPasswordStatus = Fps;
                    obj.SaveChanges();

                    bUser = AutoMapper.Mapper.Map<Users>(eUser);
                    bUser.Message = "User Exist";
                    bUser.Status = true;

                }
                else  //user does not exist
                {
                    bUser = AutoMapper.Mapper.Map<Users>(eUser);
                    bUser.Message = "User does not exist";
                    bUser.Status = false;
                }
            }
            return bUser;
        }

        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> Update User Detail</returns>
        public Users UpdateUserDetail(Users login)
        {
            User eUser = new User();
            Users bUser = new Users();
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.Id == login.Id).FirstOrDefault();
                eUser.Name = login.Name;
                eUser.Email = login.Email;
                eUser.Address = login.Address;
                eUser.Phone = login.Phone;
                obj.SaveChanges();
                bUser = login;
                bUser.Message = "Update Success";
                bUser.Status = true;
            }

            return bUser;
        }

        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> Update User Detail</returns>
        public ChangePasswordModel ChangePassword(ChangePasswordModel pass)
        {
            User eUser = new User();
            ChangePasswordModel bUser = new ChangePasswordModel();
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.Id == pass.Id && l.Password == pass.OldPassword).FirstOrDefault();
                eUser.Password = pass.NewPassword;
                obj.SaveChanges();
                bUser = pass;
                bUser.Message = "Update Success";
                bUser.Status = true;
            }

            return bUser;
        }

        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> Update User Detail</returns>
        public NewPasswordModel NewPassword(NewPasswordModel pass)
        {
            User eUser = new User();
            NewPasswordModel bPass = new NewPasswordModel();
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.ForgetPasswordCode == new Guid(pass.ForgetPassUniqueCode) && l.ForgetPasswordStatus == true).FirstOrDefault();
                eUser.Password = pass.NewPassword;
                eUser.LastPasswordChangedDate = DateTime.Now;
                eUser.ForgetPasswordStatus = false;
                obj.SaveChanges();
                bPass.List = eUser;
                bPass.Message = "Update Success";
                bPass.Status = true;
            }

            return bPass;
        }

        /// <summary>
        /// Get ForgetPassword Status//
        /// </summary>        
        /// <returns>Result</returns>
        public List<PasswordStatus> GetForgetPasswordStatus(Guid Fpc)
        {
            using (obj = new MotorMechsEntities())
            {
                //GetForgetPasswordStatus_Result bUser = new GetForgetPasswordStatus_Result();
                //bUser = (from v in obj.GetForgetPasswordStatus(Fpc) select v);
                //return bUser;
                return (obj.Database.SqlQuery<PasswordStatus>(
                  "GetForgetPasswordStatus @param1",
                     new SqlParameter("param1", Fpc)
                    )
                ).ToList();
            }
        }

        /// <summary>
        /// this method is used for check if the user exist 
        /// </summary>
        /// <param name="login"> get the username, password, rememberme</param>
        /// <returns> user detail</returns>
        public Users CheckLogin(Login login)
        {
            // User eUser = new User();
            Users bUser = new Users();
            using (obj = new MotorMechsEntities())
            {
                bUser = obj.Users.Join(obj.roles, u => u.RoleId, uir => uir.Id,
                                  (u, uir) => new { u, uir }).Where(l => ((l.u.Phone == login.Username && l.u.Password == login.Password)))
                                  .Select(r => new Users
                                  {
                                      Id = r.u.Id,
                                      Name = r.u.Name,
                                      Address = r.u.Address,
                                      Email = r.u.Email,
                                      Phone = r.u.Phone,
                                      Password = r.u.Password,
                                      CreatedBy = r.u.CreatedBy,
                                      IsActive = r.u.IsActive,
                                      RoleId = r.u.RoleId.Value,
                                      RoleType = r.uir.Name,
                                  }).FirstOrDefault();//l.Email == login.Username && l.Password == login.Password) ||
            }
            if (bUser != null)
            {
                if (!bUser.IsActive.Value) // user blocked
                {
                    //bUser = AutoMapper.Mapper.Map<Users>(eUser);
                    bUser.Message = "User login is blocked";
                    bUser.Status = true;
                }
                //else if (!eUser.IsAdminCreated.Value) // user is createdby admin has no login detail
                //{
                //    bUser = AutoMapper.Mapper.Map<Users>(eUser);
                //    bUser.Message = "User  does not exist";
                //    bUser.Status = false;
                //}
                else // user login is approved
                {
                    //bUser = AutoMapper.Mapper.Map<Users>(eUser);
                    bUser.Message = "User Exist";
                    bUser.Status = true;
                }
            }
            else  //user does not exist
            {

                //bUser = AutoMapper.Mapper.Map<Users>(eUser);
                bUser = new Users();
                bUser.Message = "User does not exist";
                bUser.Status = false;
            }
            return bUser;
        }

        /// <summary>
        /// Create User Role
        /// </summary>
        /// <param name="role"> Send RoleName and Is active</param>
        /// <returns></returns>
        public Role CreateRole(Role role)
        {
            Role bRole = new Role();
            role eRole = new role();
            using (obj = new MotorMechsEntities())
            {
                eRole = obj.roles.Where(u => u.Name.ToLower() == role.Name.ToLower()).SingleOrDefault();
                if (eRole != null)
                {
                    bRole = AutoMapper.Mapper.Map<Role>(eRole);
                    bRole.Message = "User Already Exist";
                    bRole.Status = false;
                }
                else // if not exist then create new user
                {
                    eRole = AutoMapper.Mapper.Map<role>(bRole);
                    obj.roles.Add(eRole);
                    obj.SaveChanges();
                    bRole = AutoMapper.Mapper.Map<Role>(eRole);
                    bRole.Message = "New Role Created";
                    bRole.Status = true;
                }
            }
            return bRole;
        }

        /// <summary>
        /// Update User Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role UpdateRole(Role role)
        {
            Role bRole = new Role();
            role eRole = new role();
            using (obj = new MotorMechsEntities())
            {
                eRole = AutoMapper.Mapper.Map<role>(role);
                //obj.roles.Where(r => r.Id == role.Id).SingleOrDefault();
                obj.roles.Add(eRole);
                obj.SaveChanges();
                bRole.Message = "Role Updated";
                bRole.Status = true;
            }
            return bRole;
        }

        /// <summary>
        /// create New user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Users CreateUser(Users user)
        {
            int CreatedBy = 0;
            string RoleType = "";
            int RoleId = 0;
            // check if created by empty or not//
            // if empty then user
            // if not empty then manage or admin
            CreatedBy = user.CreatedBy == 0 ? CreatedBy : Convert.ToInt32(user.CreatedBy);
            //check if the role is empty or not
            RoleType = string.IsNullOrEmpty(user.RoleType) ? "User" : user.RoleType;

            // ViewModel user to return values
            Users usrs = new Users();
            // initializing the ORM object
            using (obj = new MotorMechsEntities())
            {
                User usr = new User();
                // select the user
                usr = obj.Users.Where(u => u.Phone == user.Phone).SingleOrDefault();//|| u.Email == user.Email
                RoleId = obj.roles.Where(u => u.Name.ToLower() == RoleType.ToLower()).Select(r => r.Id).SingleOrDefault();
                if (usr != null)
                { // if user exist
                    if (usr.IsAdminCreated == true) // if user is created by admin
                    {
                        User eUser = AutoMapper.Mapper.Map<User>(user);
                        eUser.Id = usr.Id;
                        eUser.RoleId = RoleId;
                        eUser.IsAdminCreated = false;
                        obj.Users.Add(eUser);
                        obj.SaveChanges();
                        usrs = AutoMapper.Mapper.Map<Users>(usr);
                        usrs.RoleType = RoleType;
                        usrs.Message = "New User Created";
                        usrs.Status = true;
                    }
                    else // user already exists
                    {
                        usrs = AutoMapper.Mapper.Map<Users>(usr);
                        usrs.Message = "User Already Exist";
                        usrs.Status = false;
                    }
                }
                else // if not exist then create new user
                {
                    User eUser = AutoMapper.Mapper.Map<User>(user);
                    eUser.CreatedDate = DateTime.Now;
                    eUser.CreatedBy = CreatedBy;
                    eUser.RoleId = RoleId;
                    eUser.IsActive = true;
                    eUser.IsAdminCreated = RoleType == "User" ? false : true;
                    obj.Users.Add(eUser);
                    obj.SaveChanges();
                    usrs = AutoMapper.Mapper.Map<Users>(eUser);
                    usrs.RoleType = RoleType;
                    usrs.Message = "New User Created";
                    usrs.Status = true;
                }
            }
            return usrs;
        }

        public Vehicles AddVehicle(Vehicles vehicles)
        {
            Vehicles bVehicles = new Vehicles();
            using (obj = new MotorMechsEntities())
            {
                Vehicle eVehicle = new Vehicle();
                if (vehicles.Id == 0)
                {
                    eVehicle = obj.Vehicles.Where(v => v.Number.ToLower() == vehicles.Number.ToLower()).SingleOrDefault();
                    if (eVehicle != null)// already exist
                    {
                        bVehicles = AutoMapper.Mapper.Map<Vehicles>(eVehicle);
                        bVehicles.Message = "Vehicle Already Exist";
                        bVehicles.Status = false;
                    }
                    else // add new vehicle 
                    {
                        eVehicle = AutoMapper.Mapper.Map<Vehicle>(vehicles);
                        eVehicle.CreatedDate = DateTime.Now;
                        eVehicle.IsActive = true;
                        obj.Vehicles.Add(eVehicle);
                        obj.SaveChanges();
                        bVehicles = AutoMapper.Mapper.Map<Vehicles>(eVehicle);
                        bVehicles.Message = "New Vehicle Created";
                        bVehicles.Status = true;
                    }
                }
                else
                {
                    eVehicle = obj.Vehicles.Where(v => v.Id == vehicles.Id).SingleOrDefault();

                    eVehicle.Name = vehicles.Name;
                    eVehicle.Number = vehicles.Number;
                    eVehicle.ManufacturedBy = vehicles.ManufacturedBy;
                    eVehicle.Model = vehicles.Model;
                    //eVehicle = AutoMapper.Mapper.Map<Vehicle>(vehicles);
                    //eVehicle.CreatedDate = DateTime.Now;
                    //eVehicle.IsActive = true;
                    obj.SaveChanges();
                    bVehicles = AutoMapper.Mapper.Map<Vehicles>(eVehicle);
                    bVehicles.Message = "New Vehicle Created";
                    bVehicles.Status = true;
                }
            }
            return bVehicles;
        }

        public Vehicles DeleteVehicle(int Id, int UserId)
        {
            Vehicles bVehicles = new Vehicles();
            using (obj = new MotorMechsEntities())
            {
                Vehicle eVehicle = new Vehicle();
                List<Vehicle> Vehiclelst = new List<Vehicle>();
                eVehicle = obj.Vehicles.Where(v => v.Id == Id & v.Owner == UserId).SingleOrDefault();
                obj.Vehicles.Remove(eVehicle);
                obj.SaveChanges();
                Vehiclelst = obj.Vehicles.Where(v => v.Owner == UserId).ToList();
                bVehicles.List = AutoMapper.Mapper.Map<List<Vehicles>>(Vehiclelst);
                bVehicles.Message = "Vehicle Deleted";
                bVehicles.Status = true;

            }
            return bVehicles;
        }

        public Services GetServices()
        {
            Services bServices = new Services();
            List<Services> bServicesList = new List<Services>();
            using (obj = new MotorMechsEntities())
            {
                Service eServices = new Service();
                List<Service> eServicesList = obj.Services.OrderBy(o => new { o.OrderBy, o.Parent, o.SortBy }).ToList();
                if (eServicesList.Count > 0)// Service Persent
                {
                    bServicesList = AutoMapper.Mapper.Map<List<Services>>(eServicesList);
                    bServices.ListService = bServicesList;
                    bServices.Message = "Record Saved";
                    bServices.Status = true;
                }
                else // No Services 
                {
                    bServices.Message = "Some Error Occured";
                    bServices.Status = false;
                }
            }
            return bServices;
        }

        public Services AddServices(NewServiceModel NewService)
        {
            List<ServiceIds> Services = NewService.Services;// (Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceIds>>(array[0].ToString()));
            Vehicles bVehicle = NewService.VehicleViewModel;// Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicles>(array[1].ToString());
            string PickNDrop = NewService.PickNDrop;// Newtonsoft.Json.JsonConvert.DeserializeObject(array[2].ToString()).ToString();
            int UserId = NewService.UserId;// Convert.ToInt32(Newtonsoft.Json.JsonConvert.DeserializeObject(array[3].ToString()));
            int vehicelId = 0;
            Services t = new Services();
            // Starting the transaction
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    //creating contaxt
                    using (obj = new MotorMechsEntities())
                    {
                        //using (var dbContextTransaction = obj.Database.Connection.BeginTransaction())
                        // {
                        // checking the vehicle 
                        if (bVehicle.Id == 0)
                        {
                            Vehicle v = obj.Vehicles.Where(s => s.Owner == UserId && s.Number == bVehicle.Number).SingleOrDefault();
                            if (v == null) // vehicle not present for this user
                            {
                                var eVehicles = AutoMapper.Mapper.Map<Vehicle>(bVehicle);
                                eVehicles.CreatedDate = DateTime.Now;
                                eVehicles.CreatedBy = UserId;
                                eVehicles.Owner = UserId;
                                eVehicles.IsActive = true;
                                obj.Vehicles.Add(eVehicles);
                                obj.SaveChanges();
                                vehicelId = eVehicles.Id;
                            }
                            else
                                vehicelId = v.Id;
                        }
                        else
                        {
                            vehicelId = bVehicle.Id;
                        }
                        // Save data into the service model
                        UserService uService = new UserService();
                        uService.UserId = UserId;
                        uService.VehicleId = vehicelId;
                        uService.CreatedDate = DateTime.Now;
                        uService.PickNDrop = PickNDrop == "True" ? true : false;
                        uService.IsActive = true;
                        obj.UserServices.Add(uService);
                        obj.SaveChanges();
                        int OrderServiceId = uService.Id;
                        // Saving the SeriveDetails
                        foreach (ServiceIds ss in Services)
                        {
                            UserServicesDetail serviceDetail = new UserServicesDetail();
                            serviceDetail.ParentServiceId = Convert.ToInt32(ss.parent);
                            serviceDetail.UserSeviceId = Convert.ToInt32(ss.id);
                            serviceDetail.OrderServiceId = OrderServiceId;
                            serviceDetail.CreatedBy = UserId;
                            serviceDetail.IsActive = true;
                            serviceDetail.CreatedDate = DateTime.Now;
                            obj.UserServicesDetails.Add(serviceDetail);
                            obj.SaveChanges();
                        }
                        // Completing the transaction
                        ts.Complete();
                        t.Message = "Record Saved";
                        t.Status = true;
                        // }
                    }
                }
                catch (Exception e)
                {
                    t.Message = "Some error occured";
                    t.Status = false;
                }
            }

            return t;
        }

        public Vehicles getVehicleDetail(int userId)
        {
            Vehicle v = new Vehicle();
            Vehicles bV = new Vehicles();
            List<Vehicles> bVList = new List<Vehicles>();

            using (obj = new MotorMechsEntities())
            {
                List<Vehicle> list = obj.Vehicles.Where(s => s.Owner == userId).ToList();
                if (list.Count > 0)// Service Persent
                {
                    bVList = AutoMapper.Mapper.Map<List<Vehicles>>(list);
                    bV.List = bVList;
                    bV.Message = "Record Found";
                    bV.Status = true;
                }
                else // No Services 
                {
                    bV.List = bVList;
                    bV.Message = "No Record Found";
                    bV.Status = false;
                }
            }
            return bV;
        }

        public Vehicles getVehicleDetailById(int Id, int userId)
        {
            Vehicle v = new Vehicle();
            Vehicles bV = new Vehicles();
            List<Vehicles> bVList = new List<Vehicles>();
            try
            {
                using (obj = new MotorMechsEntities())
                {
                    List<Vehicle> list = obj.Vehicles.Where(s => s.Id == Id && s.Owner == userId).ToList();
                    if (list.Count > 0)// Service Persent
                    {
                        bVList = AutoMapper.Mapper.Map<List<Vehicles>>(list);
                        bV.List = bVList;
                        bV.Message = "Record Found";
                        bV.Status = true;
                    }
                    else // No Services 
                    {
                        bV.List = bVList;
                        bV.Message = "No Record Found";
                        bV.Status = true;
                    }
                }
            }
            catch (Exception e)
            {
                bV.List = null;
                bV.Message = "Some Error Occured";
                bV.Status = false;
            }
            return bV;
        }

        public UserServiceList GetUserServices(int userId)
        {
            UserService eUserService = new UserService();
            UserServiceList bUserService = new UserServiceList();
            List<UserServiceList> bUserServiceList = new List<UserServiceList>();

            try
            {
                using (obj = new MotorMechsEntities())
                {
                    bUserServiceList = obj.Vehicles.Join(obj.UserServices, v => v.Id, us => us.VehicleId, (v, us) => new { v, us })
                        .Where(u => (u.us.UserId == userId && u.us.IsActive == true))
                        .Select(z => new UserServiceList
                        {
                            OrderId = z.us.Id,
                            UserId = z.us.UserId.Value,
                            PickNDrop = z.us.PickNDrop.HasValue? z.us.PickNDrop.Value:false,
                            VehicleId = z.us.VehicleId.Value,
                            VehicleName = z.v.Name,
                            VehicleNumber = z.v.Number,
                            CreatedDate = z.us.CreatedDate.Value

                        }).ToList();


                    if (bUserServiceList.Count > 0)// Service Persent
                    {

                        bUserService.List = bUserServiceList;
                        bUserService.Message = "Record Found";
                        bUserService.Status = true;
                    }
                    else // No Services 
                    {
                        bUserService.List = null;
                        bUserService.Message = "No Record Found";
                        bUserService.Status = true;
                    }
                }
            }
            catch (Exception e)
            {
                bUserService.List = null;
                bUserService.Message = "Some Error Occured";
                bUserService.Status = false;
            }
            return bUserService;
        }

        public UserServiceDetailList GetUserServiceDetail(int OrderId)
        {
            UserServicesDetail eUserServiceDetail = new UserServicesDetail();
            UserServiceDetailList bUserServiceDetail = new UserServiceDetailList();
            List<UserServiceDetailList> bUserServiceDetailList = new List<UserServiceDetailList>();
            try
            {


                using (obj = new MotorMechsEntities())
                {
                    bUserServiceDetailList = obj.Services.Join(obj.UserServicesDetails, s => s.Id, usd => usd.UserSeviceId,
                        (s, usd) => new { s, usd })
                        .Where(u => (u.usd.OrderServiceId == OrderId && u.usd.IsActive == true))
                        .Select(z => new UserServiceDetailList
                        {
                            UserSeviceId = z.usd.Id,
                            UserSeviceName = z.s.Name,
                            OrderServiceId = z.usd.OrderServiceId.Value
                        }).ToList();


                    if (bUserServiceDetailList.Count > 0)// Service Persent
                    {

                        bUserServiceDetail.List = bUserServiceDetailList;
                        bUserServiceDetail.Message = "Record Found";
                        bUserServiceDetail.Status = true;
                    }
                    else // No Services 
                    {
                        bUserServiceDetail.List = bUserServiceDetailList;
                        bUserServiceDetail.Message = "No Record Found";
                        bUserServiceDetail.Status = true;
                    }
                }
            }
            catch (Exception e)
            {
                bUserServiceDetail.List = null;
                bUserServiceDetail.Message = "Some Error Occued";
                bUserServiceDetail.Status = false;
            }
            return bUserServiceDetail;
        }

        //public Vehicles GetUserService()
        //{
        //    //using (TransactionScope ts = new TransactionScope())
        //    //{
        //    //}
        //    using (obj = new MotorMechsEntities())
        //    {
        //        using (var dbContextTransaction = obj.Database.Connection.BeginTransaction())
        //        { 

        //        }
        //    }
        //}


        #endregion

        #region Dispose Mehod
        public void Dispose()
        {
            //    //obj.Dispose();
            //    this.Dispose();
        }
        #endregion





    }
}
