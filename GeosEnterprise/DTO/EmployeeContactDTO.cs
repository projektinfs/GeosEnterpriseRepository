using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class EmployeeContactDTO
    {
        public string Www { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public static EmployeeContactDTO ToDTO(DBO.EmployeeContact entity)
        {
            return new EmployeeContactDTO
            {
               Www = entity.Www,
               Phone = entity.Phone,
               Fax = entity.Fax,
            };
        }
    }
}
