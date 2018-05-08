using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;

namespace GeosEnterprise.Repositories
{
    public class RepairsRepository : BaseRepository<Repair, RepairDTO>
    {
        public new static void Update(RepairDTO repair)
        {
            BaseRepository<Client, ClientDTO>.Update(repair.Client);
            BaseRepository<Computer, ComputerDTO>.Update(repair.Computer);
            BaseRepository<Repair, RepairDTO>.Update(repair);
        }

        public new static IList<Repair> GetAllCurrent()
        {
            return BaseRepository<Repair>.GetAllCurrent().Where(p => p.RealizationDate == null).ToList();
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
            || p.Computer.SerialNumber.Contains(filter) 
            || p.Computer.Name.Contains(filter) 
            || p.Client.Name.Contains(filter)).ToList();

            });
        }

        public static IList<Repair> GetByTimeAndDescription(string filter, DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                IList<Repair> Repairs = Where(p => p.Computer.CreatedDate >= timeFrom
                    && p.Computer.CreatedDate <= timeTo).ToList();

                return Repairs.Where(p => p.Description.Contains(filter)
            || p.Computer.SerialNumber.Contains(filter) || p.Computer.Name.Contains(filter) || p.Client.Name.Contains(filter)).ToList();
            });
        }


        public static IList<Repair> GetAll(string filter, DateTime? TimeFrom, DateTime? TimeTo)
        {
            if (filter == "Wpisz tekst...")
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetAllCurrent();
                else
                    return GetByTime(TimeFrom, TimeTo);
            }
            else
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetByDescription(filter);
                else
                    return GetByTimeAndDescription(filter, TimeFrom, TimeTo);
            }
        }

        public static Repair Add(Repair repair)
        {
            return ExecuteQuery(() =>
            {
                repair.Client = null;
                return Insert(repair);
            });
        }

        public static void Edit(Repair repair)
        {
            ExecuteQuery(() =>
            {
                Update(repair);
            });
        }

        public static void Edit(RepairDTO repair)
        {
            ExecuteQuery(() =>
            {
                Update(repair);
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
