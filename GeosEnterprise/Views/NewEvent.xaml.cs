using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.ViewModel;
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
using WpfScheduler;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        public NewEvent()
        {
            InitializeComponent();
            DataContext = new NewEventViewModel();

            setColors();
            setEmployeesToComboBox();



            /*
            repairComboBox.DisplayMemberPath = "Key";
            repairComboBox.SelectedValuePath = "Value";

            List<RepairDTO> repairs = getRepairs();

            foreach (RepairDTO repair in repairs)
            {
                repairComboBox.Items.Add(new KeyValuePair<string, string>(repair.Computer.Name + " " + repair.Computer.SerialNumber , repair.Computer.SerialNumber));
            }
            */

        }

        public Event Event { get; internal set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if( TimeFrom == null || TimeTo == null || eventName.Text == "" || employeeComboBox.SelectedValue == null)
            {
                MessageBox.Show($"Nie wybrano poprawnie wszystkich wymaganych wartości!",
                    "Błąd!", MessageBoxButton.OKCancel);
                return;
            }
                
            Event = new Event()
            {
                Subject = eventName.Text,
                Color = (SolidColorBrush)colorBox.SelectedValue,
                Start = (DateTime)TimeFrom.Value,
                End = (DateTime)TimeTo.Value
            };

            var names = employeeComboBox.SelectedValue.ToString().Split(' ');
            string firstName = names[0];
            string lastName = names[1];

            Employee employee = Repositories.EmployeeRepository.GetByNameAndSurname(firstName, lastName);
        
            EmployeeActivity activity = new EmployeeActivity
            {
                TimeFrom = (DateTime)TimeFrom.Value,
                TimeTo = (DateTime)TimeTo.Value,
                Description = firstName + " " + lastName + " " + eventName.Text,
                EmployeeID = employee.ID
            };

            Repositories.EmployeeActivityRepository.Insert(activity);

            this.Close();
        }

        public List<EmployeeDTO> getEmployees()
        {
            return Repositories.EmployeeRepository.GetAllCurrent().Select(p => EmployeeDTO.ToDTO(p)).ToList();
        }

        /*
        public List<EmployeeDTO> getEmployeesByID()
        {
            //return Repositories.EmployeeRepository.GetAllCurrent().Where(p => p. == DBO.RepairStatus.Completed).ToList();
        }
        */

        public void setEmployeesToComboBox()
        {
            employeeComboBox.DisplayMemberPath = "Key";
            employeeComboBox.SelectedValuePath = "Value";

            List<EmployeeDTO> employees = getEmployees();

            foreach (EmployeeDTO employee in employees)
            {
                employeeComboBox.Items.Add(new KeyValuePair<string, string>(employee.FullName, employee.FullName));
            }
        }

        public void setColors()
        {
            colorBox.DisplayMemberPath = "Key";
            colorBox.SelectedValuePath = "Value";
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Czerwony", Brushes.Red));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Niebieski", Brushes.Blue));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Zielony", Brushes.Green));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Żółty", Brushes.Yellow));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Pomarańczowy", Brushes.Orange));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Karmazynowy", Brushes.Crimson));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Écru", Brushes.Beige));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Śliwkowy", Brushes.Plum));
            colorBox.Items.Add(new KeyValuePair<string, SolidColorBrush>("Polska leśna zieleń", Brushes.ForestGreen));
            colorBox.SelectedIndex = 0;
        }
    }
}
