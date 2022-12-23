using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ApiSms
    {
        public int id { get; set; }
        public string UserApiKey { get; set; }
        public string SecretKey { get; set; }
        public string Khat { get; set; }
    }
}
