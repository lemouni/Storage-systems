using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Reminder
    {
        public Reminder()
        {
            IsDone = false;
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public string ReminderInfo { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public DateTime RemindDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsDone { get; set; }
        public User Users { get; set; } 


    }
}
