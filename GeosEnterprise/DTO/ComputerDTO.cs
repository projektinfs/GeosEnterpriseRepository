using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class ComputerDTO : DTOObject<int>
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
    }
}
