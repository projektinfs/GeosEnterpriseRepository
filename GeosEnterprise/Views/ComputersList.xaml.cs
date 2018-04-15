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
                MessageBox.Show("Dodano nowe zlecenie!");
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
            Window addNewRepairWindow = new ComputersAdd(repairDTO.ID);

            if (addNewRepairWindow.ShowDialog() == true)
            {
                MessageBox.Show("Edytowano zlecenie!");
            }
        }
    }
}
