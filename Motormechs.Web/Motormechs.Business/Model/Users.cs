using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class Users: BaseModel
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public string Address  {get;set;}
        public string Email  {get;set;}
        public string Phone  {get;set;}
        public string Password {get;set;}
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int RoleId { get; set; }
        public string RoleType {get;set;}
        public Nullable<Guid> ForgetPasswordCode { get; set; }
        public Nullable<DateTime> ForgetPasswordDate { get; set; }
        public Nullable<bool> ForgetPasswordStatus { get; set; }
    }
}
