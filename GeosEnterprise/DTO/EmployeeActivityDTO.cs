using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class EmployeeActivityDTO : DTOObject<int>
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int? RepairID { get; set; }
        public string Description { get; set; }

        public string Duration
        {
            get
            {
                return TimeTo.Subtract(TimeFrom).ToString().Split('.')[0].ToString();
            }
        }

        public static EmployeeActivityDTO ToDTO(DBO.EmployeeActivity entity)
        {
            return new EmployeeActivityDTO
            {
                Description = entity.Description,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                RepairID = entity.RepairID,
            };
        }

        public static EmployeeActivity FromDTO(DTO.EmployeeActivityDTO entity)
        {
            return new EmployeeActivity
            {
                Description = entity.Description,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                RepairID = entity.RepairID,
            };
        }

    } 
}
