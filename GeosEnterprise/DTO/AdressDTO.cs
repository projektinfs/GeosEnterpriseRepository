using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class AdressDTO
    {
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string District { get; set; }
        public int PostCode { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int? AppartamentNumber { get; set; }


        public static AdressDTO ToDTO(DBO.Adress entity)
        {
            return new AdressDTO
            {
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
