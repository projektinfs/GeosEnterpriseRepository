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
        public new static RepairDTO Update(RepairDTO repair)
        {
            repair.ClientID = repair.Client.ID;
            repair.ComputerID = repair.Computer.ID;
            repair.DealerID = repair.Dealer.ID;
            BaseRepository<Computer, ComputerDTO>.Update(repair.Computer);
            return BaseRepository<Repair, RepairDTO>.Update(repair);
        }

        public new static IList<Repair> GetAllCurrent()
        {
            return BaseRepository<Repair>.GetAllCurrent().Where(p => p.RealizationDate == null).ToList();
        }


        public new static IList<Repair> GetAllCompleted()
        {
            return BaseRepository<Repair>.GetAllCurrent().Where(p => p.Status == DBO.RepairStatus.Completed).ToList();
        }


        public static Repair Add(Repair repair)
        {
            repair.Client = null;
            repair.Dealer = null;
            return Insert(repair);
        }
    }
}
