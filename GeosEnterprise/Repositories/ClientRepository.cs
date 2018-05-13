using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        public static Client GetByEmail(string Email)
        {
            return ExecuteQuery(() =>
            {
                return App.DB.Clients.Where(p => p.ClientContact.Email == Email).FirstOrDefault();
            });
            
        }

        public static Client Add(Client client)
        {
            return ExecuteQuery(() =>
            {
                return Insert(client);
            });
        }

        public static Client Edit(Client client)
        {
            return ExecuteQuery(() =>
            {
                var toEdit = App.DB.Clients.Where(p => p.ID == client.ID).FirstOrDefault();
                toEdit.ModifiedBy = Session.Username;
                toEdit.ModifiedDate = DateTime.Now;
                toEdit.Name = client.Name;
                toEdit.Surname = client.Surname;
                toEdit.ClientAdress.City = client.ClientAdress.City;
                toEdit.ClientAdress.Voivodeship = client.ClientAdress.Voivodeship;
                toEdit.ClientAdress.District = client.ClientAdress.District;
                toEdit.ClientAdress.PostCode = client.ClientAdress.PostCode;
                toEdit.ClientAdress.Street = client.ClientAdress.Street;
                toEdit.ClientAdress.BuildingNumber = client.ClientAdress.BuildingNumber;
                toEdit.ClientAdress.AppartamentNumber = client.ClientAdress.AppartamentNumber;
                toEdit.ClientContact.Phone = client.ClientContact.Phone;
                toEdit.ClientContact.Fax = client.ClientContact.Fax;
                toEdit.ClientContact.Www = client.ClientContact.Www;
                toEdit.ClientContact.Email = client.ClientContact.Email;
                return toEdit;
            });
        }

        public static void Delete(int id)
        {
            ExecuteQuery(() =>
            {
                Delete(GetById(id));
            });
        }
    }
}
