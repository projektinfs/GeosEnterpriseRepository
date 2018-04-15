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
using GeosEnterprise.ViewModels;

namespace GeosEnterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void StartPanelButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new StartPanelViewModel();
        }

        private void ComputersListButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ComputersListViewModel();
        }

        private void SearchPanelButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SearchPanelViewModel();
        }

        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AuthenticationViewModel();
        }

        private void EmployeesListButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new EmployeesListViewModel();
        }
    }
}
