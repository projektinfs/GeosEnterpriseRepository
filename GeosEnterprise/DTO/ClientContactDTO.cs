using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
   public  class ClientContactDTO
    {
        public string Email { get; set; }
        public string Www { get; set; }
        public int Phone { get; set; }
        public int? Fax { get; set; }

        public static ClientContactDTO ToDTO(DBO.ClientContact entity)
        {
            return new ClientContactDTO
            {
               Email = entity.Email,
               Www = entity.Www,
               Phone = entity.Phone,
               Fax = entity.Fax,
            };
        }
    }
}
