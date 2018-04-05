using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public static class RepairsRepository
    {
        public static Repair GetById(int id)
        {
            return EntitiesContext.DB.Repairs.Where(p => p.ID == id).FirstOrDefault();
        }

        public static IList<Repair> GetAllCurrent()
        {
            // robimy jakies testowe obiekty
            var repairs = new List<Repair>
            {
                new Repair
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    ID = 1,
                    Description = "Opis",
                    Computer = new Computer
                    {
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Admin",
                        SerialNumber = "xxx",
                        Components = new List<Component>
                        {
                            new Component()
                            {
                                ID = 1
                            }
                        },
                    }
                },
                new Repair
                {
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedBy = "Admin",
                    ID = 2,
                    Description = "Opis",
                    RealizationDate = DateTime.Now,
                    Computer = new Computer
                    {
                        CreatedDate = DateTime.Now.AddDays(-1),
                        CreatedBy = "Admin",
                        SerialNumber = "xxx",
                        Components = new List<Component>
                        {
                            new Component()
                            {
                                ID = 1
                            }
                        },
                    }
                }
            };

            //return repairs.Where(p => p.RealizationDate == null).ToList();
            return EntitiesContext.DB.Repairs.Where(p => p.RealizationDate == null).ToList();
        }

        public static Repair Add(Repair repair)
        {
            var added = EntitiesContext.DB.Repairs.Add(repair);
            EntitiesContext.DB.SaveChanges();
            return added;
        }

        public static void Delete(Repair repair)
        {
            EntitiesContext.DB.Repairs.Remove(repair);
            EntitiesContext.DB.SaveChanges();
        }

        public static void Delete(int id)
        {
            EntitiesContext.DB.Repairs.Remove(GetById(id));
            EntitiesContext.DB.SaveChanges();
        }
    }
}
