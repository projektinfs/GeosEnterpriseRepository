using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Repair : DBObject<int>
    {
        public Repair()
        {
            Status = RepairStatus.Reported;
        }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual Computer Computer { get; set; }
        public int ComputerID { get; set; }
        public string Description { get; set; }
        public DateTime? RealizationDate { get; set; }
        public RepairStatus Status { get; set; }

        public string OrderNumber
        {
            get
            {
                return $"{ID}/{CreatedDate.Value.Year}/{CreatedDate.Value.Month}";
            }
        }

        public static IList<Repair> ForSeedToDatabase()
        {
            return new List<Repair>
            {
                new Repair
                {
                    ClientID = 1,
                    Computer = new Computer
                    {
                        Name = "Lenovo",
                        SerialNumber = "123-000-F2X"
                    },
                    Description = "Opis",
                },
                new Repair
                {
                    ClientID = 2,
                    Computer = new Computer
                    {
                        Name = "IBM",
                        SerialNumber = "047-8P9-F2X"
                    },
                    Description = "Opis",
                },
                new Repair
                {
                    ClientID = 2,
                    Computer = new Computer
                    {
                        Name = "DELL",
                        SerialNumber = "123-F2X-047"
                    },
                    Description = "Opis",
                },
            };
        }
    }
}
