using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Model
{
    public partial class Services:BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Parent { get; set; }
        public int SortBy { get; set; }
        public int OrderBy { get; set; }

        public List<Services> ListService { get; set; }

    }
}
