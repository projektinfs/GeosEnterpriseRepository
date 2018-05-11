using GeosEnterprise.DBO;
using GeosEnterprise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for ClientsAdd.xaml
    /// </summary>
    public partial class ClientsAdd : Window
    {

        int? clientID { get; set; }

        public ClientsAdd()
        {
            DataContext = new ClientsAddViewModel(null);
            InitializeComponent();
        }

        public ClientsAdd(int clientID)
        {
            DataContext = new ClientsAddViewModel(clientID);
            InitializeComponent();
        }

        public ClientsAdd(int clientID, bool IsAdminMode)
        {
            DataContext = new ClientsAddViewModel(clientID, IsAdminMode);
            InitializeComponent();
        }

        //public ClientsAdd(int clientID)
        //{
        //    InitializeComponent();
        //    this.clientID = clientID;
        //    Client client = Repositories.ClientRepository.GetById(clientID);
        //    NameTextBox.Text = client.Name;
        //    SurnameTextBox.Text = client.Surname;
        //    CityTextBox.Text = client.ClientAdress.City;
        //    VoivodeshipTextBox.Text = client.ClientAdress.Voivodeship;
        //    DistrictTextBox.Text = client.ClientAdress.District;
        //    PostCodeTextBox.Text = client.ClientAdress.PostCode.ToString();
        //    StreetTextBox.Text = client.ClientAdress.Street;
        //    BuildingTextBox.Text = client.ClientAdress.BuildingNumber.ToString();
        //    AppartamentTextBox.Text = client.ClientAdress.AppartamentNumber.ToString();
        //    PhoneTextBox.Text = client.ClientContact.Phone.ToString();
        //    FaxTextBox.Text = client.ClientContact.Fax.ToString();
        //    WwwTextBox.Text = client.ClientContact.Www;
        //    EmailTextBox.Text = client.ClientContact.Email;


        //}




        //private void OKButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if(this.clientID == null)
        //    {
        //        Repositories.ClientRepository.Add(new Client
        //        {
        //            Name = NameTextBox.Text,
        //            Surname = SurnameTextBox.Text,

        //            ClientAdress = new ClientAdress
        //            {
        //                City = CityTextBox.Text,
        //                Voivodeship = VoivodeshipTextBox.Text,
        //                District = DistrictTextBox.Text,
        //                PostCode = PostCodeTextBox.Text,
        //                Street = StreetTextBox.Text,
        //                BuildingNumber = BuildingTextBox.Text,
        //                AppartamentNumber = AppartamentTextBox.Text,
        //            },
        //            ClientContact = new ClientContact
        //            {
        //                Phone = PhoneTextBox.Text,
        //                Fax = FaxTextBox.Text,
        //                Www = WwwTextBox.Text,
        //                Email = EmailTextBox.Text,
        //            }
        //        });
        //    }

        //    else
        //    {
        //        Repositories.ClientRepository.Edit(new Client
        //        {
        //            ID = (int)clientID,
        //            Name = NameTextBox.Text,
        //            Surname = SurnameTextBox.Text,

        //            ClientAdress = new ClientAdress
        //            {
        //                City = CityTextBox.Text,
        //                Voivodeship = VoivodeshipTextBox.Text,
        //                District = DistrictTextBox.Text,
        //                PostCode = PostCodeTextBox.Text,
        //                Street = StreetTextBox.Text,
        //                BuildingNumber = BuildingTextBox.Text,
        //                AppartamentNumber = AppartamentTextBox.Text,
        //            },
        //            ClientContact = new ClientContact
        //            {
        //                Phone = PhoneTextBox.Text,
        //                Fax = FaxTextBox.Text,
        //                Www = WwwTextBox.Text,
        //                Email = EmailTextBox.Text,
        //            }
        //        });
        //    }
        //    this.Close();
        //}

        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogResult = false;
        //    return;
        //}

    }
}

