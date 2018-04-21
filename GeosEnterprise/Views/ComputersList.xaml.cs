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
    /// <summary>
    /// Interaction logic for ComputersList.xaml
    /// </summary>
    public partial class ComputersList : UserControl
    {
        public ComputersList()
        {
            InitializeComponent();
            this.DataContext = new ComputersListViewModel();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
             RepairsList.ItemsSource = RepairsRepository.GetAll(SearchBar.Text, TimeFrom.Value, TimeTo.Value);
        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox SearchBar = (TextBox)sender;
            SearchBar.Text = string.Empty;
            SearchBar.GotFocus -= SearchBar_GotFocus;
        }

    }
}