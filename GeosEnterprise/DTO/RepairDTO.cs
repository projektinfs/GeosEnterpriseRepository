using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class RepairDTO
    {
        public int ID { get; set; }
        public ComputerDTO Computer { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? RealizationDate { get; set; }

        public static RepairDTO ToDTO(DBO.Repair entity)
        {
            entity.Computer = Repositories.ComputersRepository.GetByRepairId(entity.ID, true);
            return new RepairDTO
            {
                Computer = new ComputerDTO
                {
                    Components = entity.Computer.Components.Select(p => ComponentDTO.ToDTO(p)).ToList(),
                    ID = entity.Computer.ID,
                    SerialNumber = entity.Computer.SerialNumber
                },
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ID = entity.ID,
                RealizationDate = entity.RealizationDate
            };
            
        }
    }
}
