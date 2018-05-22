using GeosEnterprise.DBO;
using System;
using System.ComponentModel;

namespace GeosEnterprise.DTO
{
    public class ClientDTO : DTOObject<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ClientAdressDTO ClientAdress { get; set; }
        public ClientContactDTO ClientContact { get; set; }

        [Description("Data utworzenia")]
        public DateTime? CreatedDate { get; set; }

        [Description("Data skasowania")]
        public DateTime? DeletedDate { get; set; }

        public string FullName
        {
            get
            {
                return $"{Name} {Surname}\r\n({ClientContact?.Email})";
            }
        }

        public static ClientDTO ToDTO(DBO.Client entity)
        {
            entity.ClientAdress = Repositories.ClientAdressesRepository.GetById(entity.ClientAdress.ID);
            entity.ClientContact = Repositories.ClientContactsRepository.GetById(entity.ClientContact.ID);


            return new ClientDTO
            {
                ClientAdress = new ClientAdressDTO
                {
                    City = entity.ClientAdress.City,
                    Voivodeship = entity.ClientAdress.Voivodeship,
                    District = entity.ClientAdress.District,
                    PostCode = entity.ClientAdress.PostCode,
                    Street = entity.ClientAdress.Street,
                    BuildingNumber = entity.ClientAdress.BuildingNumber,
                    AppartamentNumber = entity.ClientAdress.AppartamentNumber
                },
                ClientContact = new ClientContactDTO
                {
                    Www = entity.ClientContact.Www,
                    Phone = entity.ClientContact.Phone,
                    Fax = entity.ClientContact.Fax,
                    Email = entity.ClientContact.Email,
                },

                ID = entity.ID,
                Name = entity.Name,
                Surname = entity.Surname,
                CreatedDate = entity.CreatedDate,
                DeletedDate = entity.DeletedDate
            };
        }

        public static Client FromDTO(ClientDTO entity)
        {
            return new Client
            {
                ID = entity.ID,
                ClientAdress = ClientAdressDTO.FromDTO(entity.ClientAdress),
                ClientContact = ClientContactDTO.FromDTO(entity.ClientContact),
                Name = entity.Name,
                Surname = entity.Surname,
                CreatedDate = entity.CreatedDate,
                DeletedDate = entity.DeletedDate
            };
        }
    }
}
