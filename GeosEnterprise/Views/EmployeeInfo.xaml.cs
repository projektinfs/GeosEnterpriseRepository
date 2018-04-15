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
using System.Windows.Shapes;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Views
{

    public partial class EmployeeInfo : Window
    {
        public EmployeeInfo()
        {
            InitializeComponent();
        }

        int? employeeID { get; set; }


        public EmployeeInfo(int employeeID)
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


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            return;
        }
    }
}
