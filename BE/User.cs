using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public User()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Pic { get; set; }
        public bool DeleteStatus { get; set; }
        //public bool Status { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<Buy> Buys { get; set; } = new List<Buy>();
        public UserGroup UserGroup { get; set; }

    }
}
