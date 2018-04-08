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
        public DbSet<Component> Components { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
