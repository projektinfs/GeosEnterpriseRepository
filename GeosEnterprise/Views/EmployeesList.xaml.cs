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
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.ViewModels;
using GeosEnterprise.Repositories;

namespace GeosEnterprise.Views
{

    public partial class EmployeesList : UserControl
    {
        public EmployeesList()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            this.InvalidateVisual();
            this.UpdateLayout();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Window addNewEmployeeWindow = new EmployeesAdd();
            addNewEmployeeWindow.ShowDialog();

            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeDTO = AllEmployeesList.SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Repositories.EmployeeRepository.Delete(employeeDTO.ID);
                Refresh();
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeDTO = AllEmployeesList.SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeesAdd(employeeDTO.ID);
                addNewEmployeeWindow.ShowDialog();
            }

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeDTO = AllEmployeesList.SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeeInfo(employeeDTO.ID);
                addNewEmployeeWindow.ShowDialog();
            }

        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox SearchBar = (TextBox)sender;
            SearchBar.Text = string.Empty;
            SearchBar.GotFocus -= SearchBar_GotFocus;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AllEmployeesList.ItemsSource = EmployeeRepository.GetAll(SearchBar.Text, TimeFrom.Value, TimeTo.Value);
        }
    }
}

