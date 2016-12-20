using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class BaseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
        public int LoginId { get; set; }
    }
}
