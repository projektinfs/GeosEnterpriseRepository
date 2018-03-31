using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.Repositories;

namespace GeosEnterprise.ViewModels
{
    public class ComputersListViewModel
    {
        public ComputersListViewModel()
        {
            RepairsRepository.GetAllCurrent();
        }

        public static IList<GeosEnterprise.DBO.Repair> GetData()
        {
            return RepairsRepository.GetAllCurrent();
        }
    }
}
