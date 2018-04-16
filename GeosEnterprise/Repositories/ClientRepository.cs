using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public static class ClientRepository
    {
        public static Client GetByEmail(string Email)
        {
            return App.DB.Clients.Where(p => p.ClientContact.Email == Email).FirstOrDefault();
        }

        public static Client Add(Client client)
        {
            var added = App.DB.Clients.Add(client);
            App.DB.SaveChanges();
            return added;
        }

        public static Client Edit(Client client)
        {

            var toEdit = App.DB.Clients.Where(p => p.ID == client.ID).FirstOrDefault();
            toEdit.ModifiedBy = "admin";
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


            App.DB.SaveChanges();
            return toEdit;
        }

        public static Client GetById(int id)
        {
            return App.DB.Clients.Where(p => p.ID == id).FirstOrDefault();
        }

        public static IList<Client> GetAllCurrent()
        {
            return App.DB.Clients.ToList();
        }

        public static void Delete(int id)
        {
            App.DB.Clients.Remove(GetById(id));
            App.DB.SaveChanges();
        }



    }
}
