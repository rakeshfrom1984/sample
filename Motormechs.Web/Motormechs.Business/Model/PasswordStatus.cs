using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class PasswordStatus
    {
        public string status { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
    }
}
