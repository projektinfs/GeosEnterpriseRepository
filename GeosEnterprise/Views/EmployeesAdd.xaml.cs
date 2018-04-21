using GeosEnterprise.DBO;
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
    /// Interaction logic for EmployeesAdd.xaml
    /// </summary>
    public partial class EmployeesAdd : Window
    {

        int? employeeID { get; set; }

        public EmployeesAdd()
        {
            InitializeComponent();
        }

        public EmployeesAdd(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
            Employee employee = Repositories.EmployeeRepository.GetById(employeeID);
            EmailTextBox.Text = employee.Email;
            PasswordTextBox.Text = employee.Password;
            NameTextBox.Text = employee.Name;
            SurnameTextBox.Text = employee.Surname;
            PositionTextBox.Text = employee.Position;
            CityTextBox.Text = employee.Adress.City;
            VoivodeshipTextBox.Text = employee.Adress.Voivodeship;
            DistrictTextBox.Text = employee.Adress.District;
            PostCodeTextBox.Text = employee.Adress.PostCode.ToString();
            StreetTextBox.Text = employee.Adress.Street;
            BuildingTextBox.Text = employee.Adress.BuildingNumber.ToString();
            AppartamentTextBox.Text = employee.Adress.AppartamentNumber.ToString();
            PhoneTextBox.Text = employee.EmployeeContact.Phone.ToString();
            FaxTextBox.Text = employee.EmployeeContact.Fax.ToString();
            WwwTextBox.Text = employee.EmployeeContact.Www;

        }




        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.employeeID == null)
            {
                Repositories.EmployeeRepository.Add(new Employee
                {
                    Email = EmailTextBox.Text,
                    Password = PasswordTextBox.Text,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    Position = PositionTextBox.Text,
                    UserRole = PositionToUserRole(PositionTextBox.Text),
                    Adress = new Adress
                    {
                        City = CityTextBox.Text,
                        Voivodeship = VoivodeshipTextBox.Text,
                        District = DistrictTextBox.Text,
                        PostCode = PostCodeTextBox.Text,
                        Street = StreetTextBox.Text,
                        BuildingNumber = BuildingTextBox.Text,
                        AppartamentNumber = int.Parse(AppartamentTextBox.Text)
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = int.Parse(PhoneTextBox.Text),
                        Fax = int.Parse(FaxTextBox.Text),
                        Www = WwwTextBox.Text
                    }
                });
            }

            else
            {
                Repositories.EmployeeRepository.Edit(new Employee
                {
                    ID = (int)employeeID,
                    Email = EmailTextBox.Text,
                    Password = PasswordTextBox.Text,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    Position = PositionTextBox.Text,
                    UserRole = PositionToUserRole(PositionTextBox.Text),
                    Adress = new Adress
                    {
                        City = CityTextBox.Text,
                        Voivodeship = VoivodeshipTextBox.Text,
                        District = DistrictTextBox.Text,
                        PostCode = PostCodeTextBox.Text,
                        Street = StreetTextBox.Text,
                        BuildingNumber = BuildingTextBox.Text,
                        AppartamentNumber = int.Parse(AppartamentTextBox.Text)
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = int.Parse(PhoneTextBox.Text),
                        Fax = int.Parse(FaxTextBox.Text),
                        Www = WwwTextBox.Text
                    }
                });
            }



            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            return;
        }

        private UserRole PositionToUserRole(string position)
        {
            switch (position)
            {
                case "Kierownik":
                    return UserRole.Manager;
                case "Księgowy":
                    return UserRole.Accountant;
                case "Serwisant":
                    return UserRole.Serviceman;
                case "Sprzedawca":
                    return UserRole.Dealer;
                default:
                    return UserRole.Unknown;
            }
        }

    }
}
