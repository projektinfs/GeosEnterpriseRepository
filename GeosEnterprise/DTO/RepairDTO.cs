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

        [Description("Serwisant")]
        public EmployeeDTO Serviceman { get; set; }
        public int? ServicemanID { get; set; }

        [Description("Opis")]
        public string Description { get; set; }

        [Description("Data utworzenia zlecenia")]
        public DateTime? CreatedDate { get; set; }

        [Description("Osoba przyjmująca")]
        public EmployeeDTO Dealer { get; set; }

        public int DealerID { get; set; }

        [Description("Data zamknięcia zlecenia")]
        public DateTime? RealizationDate { get; set; }

        public decimal? ReplacementsCosts { get; set; }

        public decimal RepairCosts { get; set; }

        public decimal FinalCosts
        {
            get
            {
                var returnVal = RepairCosts;
                returnVal += ReplacementsCosts ?? 0;
                return returnVal;
            }
        }

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
        public RepairStatus Status { get; set; }

        public static RepairDTO ToDTO(DBO.Repair entity)
        {
            EmployeeDTO serviceman = null;
            if (entity.Serviceman != null)
            {
                serviceman = DTO.EmployeeDTO.ToDTO(entity.Serviceman);
            }

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
                Status = entity.Status > 0 ? entity.Status : RepairStatus.Reported,
                Serviceman = serviceman,
                ServicemanID = entity.ServicemanID,
                Dealer = DTO.EmployeeDTO.ToDTO(entity.Dealer),
                DealerID = entity.DealerID,
                RepairCosts = entity.RepairCosts,
                ReplacementsCosts = entity.ReplacementsCosts
            };
        }

        public static Repair FromDTO(DTO.RepairDTO entity)
        {
            Employee serviceman = null;
            if (entity.Serviceman != null)
            {
                serviceman = DTO.EmployeeDTO.FromDTO(entity.Serviceman);
            }

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
                Status = entity.Status > 0 ? entity.Status : RepairStatus.Reported,
                Serviceman = serviceman,
                ServicemanID = entity.ServicemanID,
                Dealer = DTO.EmployeeDTO.FromDTO(entity.Dealer),
                DealerID = entity.DealerID,
                RepairCosts = entity.RepairCosts,
                ReplacementsCosts = entity.ReplacementsCosts
            };
        }
    }
}
