using Motormechs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class NewPasswordModel : BaseModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public User List { get; set; }
        public string ForgetPassUniqueCode { get; set; }
    }
}
