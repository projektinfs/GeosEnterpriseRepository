using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public static class ComputersRepository
    {
        public static Computer GetById(int id, bool withComponents = false)
        {
            Computer computer = EntitiesContext.DB.Computers.Where(p => p.ID == id).FirstOrDefault();
            if (withComponents)
            {
                computer.Components = GetComponentsByComputerId(id);
            }
            return computer;
        }

        public static Computer GetByRepairId(int repairId, bool withComponents = false)
        {
            Computer computer = EntitiesContext.DB.Computers.Where(p => p.RepairID == repairId).FirstOrDefault();
            if (withComponents)
            {
                computer.Components = GetComponentsByComputerId(computer.ID);
            }
            return computer;
        }

        public static List<Component> GetComponentsByComputerId(int computerId)
        {
            return EntitiesContext.DB.Components.Where(p => p.ComputerID == computerId).ToList();
        }
    }
}
