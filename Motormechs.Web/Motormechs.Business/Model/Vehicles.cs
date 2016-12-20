using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public class Vehicles : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string ManufacturedBy { get; set; }
        public string Number { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int Owner { get; set; }
        public List<Vehicles> List { get; set; }
    }
}
