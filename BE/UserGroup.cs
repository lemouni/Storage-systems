using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UserGroup
    {
        public int id { get; set; }
        public string Title { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<UserAccessRole> UserAccessRoles { get; set; } = new List<UserAccessRole>();
    }

}
