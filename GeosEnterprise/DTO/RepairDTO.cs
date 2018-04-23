using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class RepairDTO : DTOObject<int>
    {
        public ClientDTO Client { get; set; }
        public int ClientID { get; set; }
        public ComputerDTO Computer { get; set; }
        public int ComputerID { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? RealizationDate { get; set; }

        public static RepairDTO ToDTO(DBO.Repair entity)
        {
            return new RepairDTO
            {
                Computer = DTO.ComputerDTO.ToDTO(entity.Computer),
                Client = DTO.ClientDTO.ToDTO(entity.Client),
                ClientID = entity.ClientID,
                ComputerID = entity.ComputerID,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ID = entity.ID,
                RealizationDate = entity.RealizationDate,
            };
        }

        public static Repair FromDTO(DTO.RepairDTO entity)
        {
            return new Repair
            {
                Client = ClientDTO.FromDTO(entity.Client),
                ClientID = entity.Client.ID,
                Computer = ComputerDTO.FromDTO(entity.Computer),
                ComputerID = entity.Computer.ID,
                Description = entity.Description,
                ID = entity.ID,
                CreatedDate = entity.CreatedDate,
                RealizationDate = entity.RealizationDate
            };
        }
    }
}
