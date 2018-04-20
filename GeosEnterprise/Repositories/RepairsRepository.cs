using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public class RepairsRepository : BaseRepository<Repair>
    {
        public new static IList<Repair> GetAllCurrent()
        {
            return ExecuteQuery(() =>
            {
                return BaseRepository<Repair>.GetAllCurrent().Where(p => p.RealizationDate == null).ToList();
            });
        }

        public static IList<Repair> GetByTime(DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.Computer.CreatedDate >= timeFrom && p.Computer.CreatedDate <= timeTo).ToList();
            });
        }

        public static IList<Repair> GetByDescription(string filter)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.Description.Contains(filter)
            || p.Computer.SerialNumber.Contains(filter)).ToList();
            });
        }

        public static IList<Repair> GetByTimeAndDescription(string filter, DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                IList<Repair> Repairs = Where(p => p.Computer.CreatedDate >= timeFrom
                    && p.Computer.CreatedDate <= timeTo).ToList();

                return Repairs.Where(p => p.Description.Contains(filter)
                || p.Computer.SerialNumber.Contains(filter)).ToList();
            });
        }

        public static Repair Add(Repair repair)
        {
            return ExecuteQuery(() =>
            {
                return Insert(repair);
            });
        }

        public static Repair Edit(Repair repair)
        {
            return ExecuteQuery(() =>
            {
                var toEdit = App.DB.Repairs.Where(p => p.ID == repair.ID).FirstOrDefault();
                toEdit.ModifiedBy = Session.Username;
                toEdit.ModifiedDate = DateTime.Now;
                toEdit.Description = repair.Description;
                toEdit.Computer = repair.Computer;
                toEdit.ClientID = repair.ClientID;
                App.DB.SaveChanges();
                return toEdit;
            });
        }

        public static void Delete(int id)
        {
            ExecuteQuery(() =>
            {
                Delete(GetById(id));
            });
        }
    }
}
