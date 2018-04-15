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
    public class EmployeesListViewModel
    {
        public EmployeesListViewModel()
        {
            EmployeeRepository.GetAllCurrent();
        }

        public List<EmployeeDTO> Items
        {
            get
            {
                return Repositories.EmployeeRepository.GetAllCurrent().Select(p => DTO.EmployeeDTO.ToDTO(p)).ToList();
            }
        }
    }
}

