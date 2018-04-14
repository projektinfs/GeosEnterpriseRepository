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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;

namespace GeosEnterprise.Views
{

    public partial class SearchPanel : UserControl
    {

        public SearchPanel()
        {
            InitializeComponent();

        }

        private void txtNameToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchedList.ItemsSource = RepairsRepository.GetFilteredByDescription(txtNameToSearch.Text);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
