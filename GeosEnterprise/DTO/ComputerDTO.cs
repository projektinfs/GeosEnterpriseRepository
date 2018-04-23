using GeosEnterprise.DBO;
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

        public static ComputerDTO ToDTO(DBO.Computer entity)
        {
            return new ComputerDTO
            {
                ID = entity.ID,
                Name = entity.Name,
                SerialNumber = entity.SerialNumber
            };
        }

        public static Computer FromDTO(ComputerDTO entity)
        {
            return new Computer
            {
                ID = entity.ID,
                Name = entity.Name,
                SerialNumber = entity.SerialNumber
            };
        }
    }
}
