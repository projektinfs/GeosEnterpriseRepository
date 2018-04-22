using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class EmployeeDTO : DTOObject<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public AdressDTO Adress { get; set; }
        public EmployeeContactDTO EmployeeContact { get; set; }
        public UserRole UserRole { get; set;}


        public static EmployeeDTO ToDTO(DBO.Employee entity)
        {
            entity.Adress = Repositories.AdressesRepository.GetById(entity.Adress.ID);
            entity.EmployeeContact = Repositories.EmployeeContactsRepository.GetById(entity.EmployeeContact.ID);


            return new EmployeeDTO
            {
                Adress = new AdressDTO
                {
                    City = entity.Adress.City,
                    Voivodeship = entity.Adress.Voivodeship,
                    District = entity.Adress.District,
                    PostCode = entity.Adress.PostCode,
                    Street = entity.Adress.Street,
                    BuildingNumber = entity.Adress.BuildingNumber,
                    AppartamentNumber = entity.Adress.AppartamentNumber
                },
                EmployeeContact = new EmployeeContactDTO
                {
                    Www = entity.EmployeeContact.Www,
                    Phone = entity.EmployeeContact.Phone,
                    Fax = entity.EmployeeContact.Fax,
                },
                ID = entity.ID,
               Email = entity.Email,
               Password = entity.Password,
               Name  = entity.Name,
               Surname = entity.Surname,
               Position = entity.Position,
               UserRole = entity.UserRole
            };
        }
    }
}
