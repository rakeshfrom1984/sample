using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class Role:BaseModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
