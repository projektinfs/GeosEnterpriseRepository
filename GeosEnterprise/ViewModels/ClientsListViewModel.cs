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
    public class ClientsListViewModel
    {
        public ClientsListViewModel()
        {
            ClientRepository.GetAllCurrent();
        }

        public List<ClientDTO> Items
        {
            get
            {
                return Repositories.ClientRepository.GetAllCurrent().Select(p => DTO.ClientDTO.ToDTO(p)).ToList();
            }
        }
    }
}

