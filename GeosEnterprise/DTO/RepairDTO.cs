using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class RepairDTO : DTOObject<int>
    {
        //public ClientDTO Client { get; set; }
        public ComputerDTO Computer { get; set; }
        public int ComputerID { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? RealizationDate { get; set; }

        public static RepairDTO ToDTO(DBO.Repair entity)
        {
            entity.Computer = Repositories.ComputersRepository.GetByRepairId(entity.ID);
            return new RepairDTO
            {
                Computer = new ComputerDTO
                {
                    ID = entity.Computer.ID,
                    SerialNumber = entity.Computer.SerialNumber,
                    Name = entity.Computer.Name
                },
                ComputerID = entity.ComputerID,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ID = entity.ID,
                RealizationDate = entity.RealizationDate,
            };
            
        }
    }
}
