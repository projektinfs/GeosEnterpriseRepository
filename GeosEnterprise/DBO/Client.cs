using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Client : DBObject<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ClientAdress ClientAdress { get; set; }
        public virtual ClientContact ClientContact { get; set; }

        public static IList<Client> ForSeedToDatabase()
        {
            return new List<Client>
            {
                new Client
                {
                    Name = "Paweł",
                    Surname = "Kociński",
                    ClientAdress = new ClientAdress
                    {
                        AppartamentNumber = "4",
                        BuildingNumber = "13",
                        City = "Warszawa",
                        PostCode = "35-533",
                        Street = "Mazowiecka",
                        Voivodeship = "Mazowieckie"
                    },
                    ClientContact = new ClientContact
                    {
                        Email = "kocinskipawel@gmail.com",
                        Phone = "543534234"
                    }
                },
                new Client
                {
                    Name = "Adam",
                    Surname = "Nowacki",
                    ClientAdress = new ClientAdress
                    {
                        AppartamentNumber = "2",
                        BuildingNumber = "3",
                        City = "Kraków",
                        PostCode = "35-222",
                        Street = "Mazowiecka",
                        Voivodeship = "Małopolskie"
                    },
                    ClientContact = new ClientContact
                    {
                        Email = "nowacki12@gmail.com",
                        Phone = "586542358"
                    }
                },
                new Client
                {
                    Name = "Małgorzata",
                    Surname = "Zalewska",
                    ClientAdress = new ClientAdress
                    {
                        BuildingNumber = "32",
                        City = "Sosnowiec",
                        PostCode = "30-300",
                        Street = "Malicka",
                        Voivodeship = "Śląskie"
                    },
                    ClientContact = new ClientContact
                    {
                        Email = "zalewska.m2@gmail.com",
                        Phone = "888656236"
                    }
                }
            };
        }
    }
}
