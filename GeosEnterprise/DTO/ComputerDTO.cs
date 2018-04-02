using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class ComputerDTO
    {
        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public List<ComponentDTO> Components { get; set; }
    }
}
