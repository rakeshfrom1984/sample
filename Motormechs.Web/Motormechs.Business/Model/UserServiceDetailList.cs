using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class UserServiceDetailList:BaseModel
    {
        public int UserSeviceId { get; set; }
        public string UserSeviceName { get; set; }
        public int OrderServiceId { get; set; }
       public List<UserServiceDetailList> List {get;set;}
    }
}
