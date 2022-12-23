using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UserAccessRole
    {
        public int id { get; set; }
        public string Section { get; set; }
        public bool CanEnter { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public UserGroup UserGroup { get; set; }
    }

}
