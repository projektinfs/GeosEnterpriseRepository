using GeosEnterprise.DBO;
using GeosEnterprise.ViewModels;
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
    public partial class ClientsAdd : Window
    {
        int? clientID { get; set; }

        public ClientsAdd()
        {
            DataContext = new ClientsAddViewModel(null);
            InitializeComponent();
        }

        public ClientsAdd(int clientID)
        {
            DataContext = new ClientsAddViewModel(clientID);
            InitializeComponent();
        }

        public ClientsAdd(int clientID, bool IsAdminMode)
        {
            DataContext = new ClientsAddViewModel(clientID, IsAdminMode);
            InitializeComponent();
        }
    }
}

