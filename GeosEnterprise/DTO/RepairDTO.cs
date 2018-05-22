using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class RepairDTO : DTOObject<int>
    {
        public ClientDTO Client { get; set; }
        public int ClientID { get; set; }

        [Description("Komputer")]
        public ComputerDTO Computer { get; set; }
        public int ComputerID { get; set; }

        [Description("Opis")]
        public string Description { get; set; }

        [Description("Data utworzenia zlecenia")]
        public DateTime? CreatedDate { get; set; }

        [Description("Data zamknięcia zlecenia")]
        public DateTime? RealizationDate { get; set; }

        [Description("Numer zamówienia")]
        public string OrderNumber
        {
            get
            {
                if (CreatedDate != null)
                {
                    return $"{ID}/{CreatedDate.Value.Year}/{CreatedDate.Value.Month}";
                }
                else
                {
                    return $"{ID}";
                }
            }
        }

        [Description("Status naprawy")]
        RepairStatus Status { get; set; }

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
                Status = entity.Status
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
                RealizationDate = entity.RealizationDate,
                Status = entity.Status
            };
        }
    }
}
