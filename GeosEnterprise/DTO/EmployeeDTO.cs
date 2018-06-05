using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class EmployeeDTO : DTOObject<int>
    {
        public string Email { get; set; }
        public Byte[] Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AdressDTO Adress { get; set; }
        public EmployeeContactDTO EmployeeContact { get; set; }
        public UserRole UserRole { get; set;}

        [Description("Data utworzenia")]
        public DateTime? CreatedDate { get; set; }

        [Description("Data skasowania")]
        public DateTime? DeletedDate { get; set; }

        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }

        public string Position { get; set; }

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
               UserRole = entity.UserRole,
               CreatedDate = entity.CreatedDate,
               DeletedDate = entity.DeletedDate,
               Position = entity.Position
            };
        }

        public static Employee FromDTO(DTO.EmployeeDTO entity)
        {
            return new Employee
            {
                Adress = new Adress
                {
                    City = entity.Adress.City,
                    Voivodeship = entity.Adress.Voivodeship,
                    AppartamentNumber = entity.Adress.AppartamentNumber,
                    BuildingNumber = entity.Adress.BuildingNumber,
                    District = entity.Adress.District,
                    PostCode = entity.Adress.PostCode,
                    Street = entity.Adress.Street
                },
                EmployeeContact = new EmployeeContact
                {
                    Www = entity.EmployeeContact.Www,
                    Phone = entity.EmployeeContact.Phone,
                    Fax = entity.EmployeeContact.Fax
                },

                ID = entity.ID,
                Email = entity.Email,
                Password = entity.Password,
                Name = entity.Name,
                Surname = entity.Surname,
                UserRole = entity.UserRole,
                CreatedDate = entity.CreatedDate,
                DeletedDate = entity.DeletedDate,
                Position = entity.Position
            };
        }
    }
}
