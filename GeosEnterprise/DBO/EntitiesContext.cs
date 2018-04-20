using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("GeosEnterprise.Properties.Settings.GeosEnterpriseDB")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAdress> ClientAdresses { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<EmployeeContact> EmployeeContacts { get; set; }


    }
}
