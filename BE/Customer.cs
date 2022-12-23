using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Customer
    {
        public Customer()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public bool DeleteStatus { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double CreditWithoutDocuments { get; set; }
        public double TotalCreditWithDocument { get; set; }
        public string Adress { get; set; }
        public string CodePost { get; set; }
        public string CodeMeli { get; set; }
        public string CodeEghtesadi { get; set; }
        public string AccountGroup { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<Activity> Activities { get; set; } = new List<Activity>();


    }
}
