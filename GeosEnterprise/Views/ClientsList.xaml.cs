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
   
    public partial class ClientsList : UserControl
    {
        public ClientsList()
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
            Window addNewClientWindow = new ClientsAdd();
            addNewClientWindow.ShowDialog();
           
            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ClientDTO = AllClientsList.SelectedItem as ClientDTO;
            if (clientDTO != null)
            {
                Repositories.ClientRepository.Delete(clientDTO.ID);
                Refresh();
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var clientDTO = AllClientsList.SelectedItem as ClientDTO;
            if (clientDTO != null)
            {
                Window addNewClientWindow = new ClientsAdd(clientDTO.ID);
                addNewClientWindow.ShowDialog();
            }
           
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var ClientDTO = AllClientsList.SelectedItem as ClientDTO;
            if (ClientDTO != null)
            {
                Window addNewClientWindow = new ClientInfo(clientDTO.ID);
                addNewClientWindow.ShowDialog();
            }

        }
    }
}

