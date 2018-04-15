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
            return App.DB.Repairs.Where(p => p.ID == id).FirstOrDefault();
        }

        public static IList<Repair> GetAllCurrent()
        {
            return App.DB.Repairs.Where(p => p.RealizationDate == null).ToList();
        }

        public static IList<Repair> GetByTime(DateTime? from, DateTime? to)
        {
            return App.DB.Repairs.Where(p => p.Computer.CreatedDate > from || p.Computer.CreatedDate < to).ToList();
        }

        public static IList<Repair> GetByDescription(string filter)
        {
            return App.DB.Repairs.Where(p => p.Description.Contains(filter)
            || p.Computer.SerialNumber.Contains(filter)).ToList();
        }

        public static Repair Add(Repair repair)
        {
            var added = App.DB.Repairs.Add(repair);
            App.DB.SaveChanges();
            return added;
        }

        public static Repair Edit(Repair repair)
        {
            var toEdit = App.DB.Repairs.Where(p => p.ID == repair.ID).FirstOrDefault();
            toEdit.ModifiedBy = "admin";
            toEdit.ModifiedDate = DateTime.Now;
            toEdit.Description = repair.Description;
            toEdit.Computer = repair.Computer;
            toEdit.Computer.Components = repair.Computer.Components;
            toEdit.ClientID = repair.ClientID;
            App.DB.SaveChanges();
            return toEdit;
        }

        public static void Delete(Repair repair)
        {
            App.DB.Repairs.Remove(repair);
            App.DB.SaveChanges();
        }

        public static void Delete(int id)
        {
            App.DB.Repairs.Remove(GetById(id));
            App.DB.SaveChanges();
        }
    }
}
