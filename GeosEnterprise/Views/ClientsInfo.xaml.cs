using GeosEnterprise.DBO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for ClientsAdd.xaml
    /// </summary>
    public partial class ClientsInfo : Window
    {
        public ClientsInfo()
        {
            InitializeComponent();
        }

        int? clientID { get; set; }


        public ClientsInfo(int clientID)
        {
            DataContext = new ClientsInfoViewModel(clientID);
            InitializeComponent();
        }
    }
}

