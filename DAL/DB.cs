using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BE;

namespace DAL
{
    public class DB : DbContext

    {
        public DB() : base("Constr")
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserAccessRole> UserAccessRoles { get; set; }
        public DbSet<CountProduct> CountProducts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<AnbarCategory> AnbarCategories { get; set; }
        public DbSet<CounProductInAnbar> CounProductInAnbars { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Buy_CountProduct> Buy_CountProducts { get; set; }
        public DbSet<HesabBank> HesabBanks { get; set; }
        public DbSet<BuyPardakht> BuyPardakhts { get; set; }
        public DbSet<SellDaryaft> SellDaryafts { get; set; }
        public DbSet<NoePardakht> NoePardakhts { get; set; }
        public DbSet<CategoriNoe> CategoriNoes { get; set; }




        public DbSet<MoshkhasatFactor> MoshkhasatFactors { get; set; }
        public DbSet<ApiSms> ApiSmss { get; set; }
        public DbSet<Sms> Smss { get; set; }






    }
}
