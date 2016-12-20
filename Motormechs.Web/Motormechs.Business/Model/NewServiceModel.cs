using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class NewServiceModel
    {
      public  List<ServiceIds> Services { get; set; }
      public Vehicles VehicleViewModel { get; set; }
      public string PickNDrop { get; set; }
      public int UserId { get; set; }
    }
}
