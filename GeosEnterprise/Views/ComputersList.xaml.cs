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
using GeosEnterprise.Repositories;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.ViewModels;
using Xceed.Wpf.Toolkit;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for ComputersList.xaml
    /// </summary>
    public partial class ComputersList : UserControl
    {
        public ComputersList()
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
            Window addNewRepairWindow = new ComputersAdd();
            if (addNewRepairWindow.ShowDialog() == true)
            {
                System.Windows.MessageBox.Show("Dodano nowe zlecenie!");
            }

            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var repairDTO = RepairsList.SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                Repositories.RepairsRepository.Delete(repairDTO.ID);
                Refresh();
            }
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var repairDTO = RepairsList.SelectedItem as RepairDTO;

            if (repairDTO == null)
                return;

            Window addNewRepairWindow = new ComputersAdd(repairDTO.ID);

            if (addNewRepairWindow.ShowDialog() == true)
            {
                System.Windows.MessageBox.Show("Edytowano zlecenie!");
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if( SearchBar.Text != "Wpisz numer seryjny lub opis...")
            {
                RepairsList.ItemsSource = RepairsRepository.GetByDescription(SearchBar.Text);
            }

        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= SearchBar_GotFocus;
        }

        private void SearchOptions_Click(object sender, RoutedEventArgs e)
        {
            Window addTimePickerWindows = new TimePicker();

            if (addTimePickerWindows.ShowDialog() == true)
            {
                
                System.Windows.MessageBox.Show("Wybrano dodatkowe kryteria wyszukiwania.");
            }
        }
    }
}
