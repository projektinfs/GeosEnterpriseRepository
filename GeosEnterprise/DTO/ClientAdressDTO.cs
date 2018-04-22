using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class ClientAdressDTO : DTOObject<int>
    {
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string District { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public int? AppartamentNumber { get; set; }


        public static ClientAdressDTO ToDTO(DBO.ClientAdress entity)
        {
            return new ClientAdressDTO
            {
                ID = entity.ID,
                City = entity.City,
                Voivodeship = entity.Voivodeship,
                District = entity.District,
                PostCode = entity.PostCode,
                Street = entity.Street,
                BuildingNumber = entity.BuildingNumber,
                AppartamentNumber = entity.AppartamentNumber,
            };
        }
    }
}
