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
            Computer computer = App.DB.Computers.Where(p => p.ID == id).FirstOrDefault();
            if (withComponents)
            {
                computer.Components = GetComponentsByComputerId(id);
            }
            return computer;
        }

        public static Computer GetByRepairId(int repairId, bool withComponents = false)
        {
            var repair = Repositories.RepairsRepository.GetById(repairId);
            Computer computer = App.DB.Computers.Where(p => p.ID == repair.ComputerID).FirstOrDefault();
            if (withComponents)
            {
                computer.Components = GetComponentsByComputerId(computer.ID);
            }
            return computer;
        }

        public static List<Component> GetComponentsByComputerId(int computerId)
        {
            return App.DB.Components.Where(p => p.ComputerID == computerId).ToList();
        }
    }
}
