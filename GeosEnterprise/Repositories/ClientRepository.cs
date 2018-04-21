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

        public static IList<Client> GetByTime(DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.CreatedDate >= timeFrom
                    && p.CreatedDate <= timeTo).ToList();
            });
        }

        public static IList<Client> GetByDescription(string filter)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.Name.Contains(filter)
                || p.Surname.Contains(filter) || p.ClientContact.Email.Contains(filter) || p.ClientContact.Phone.Contains(filter)).ToList();
            });
        }

        public static IList<Client> GetByTimeAndDescription(string filter, DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                IList<Client> clients = Where(p => p.CreatedDate >= timeFrom
                    && p.CreatedDate <= timeTo).ToList();

                return clients.Where(p => p.Name.Contains(filter)
                || p.Surname.Contains(filter) || p.ClientContact.Email.Contains(filter) || p.ClientContact.Phone.Contains(filter)).ToList();
            });
        }

        public static IList<Client> GetAll(string filter, DateTime? TimeFrom, DateTime? TimeTo)
        {
            if (filter == "Wpisz tekst...")
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetAllCurrent();
                else
                    return GetByTime(TimeFrom, TimeTo);
            }
            else
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetByDescription(filter);
                else
                    return GetByTimeAndDescription(filter, TimeFrom, TimeTo);
            }
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
