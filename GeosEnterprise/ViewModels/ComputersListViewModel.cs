using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;

namespace GeosEnterprise.ViewModels
{
    public class ComputersListViewModel
    {
        public ComputersListViewModel()
        {
            RepairsRepository.GetAllCurrent();
        }

        public List<RepairDTO> Items
        {
            get
            {
                return Repositories.RepairsRepository.GetAllCurrent().Select(p => DTO.RepairDTO.ToDTO(p)).ToList();
            }
        }
    }
}
