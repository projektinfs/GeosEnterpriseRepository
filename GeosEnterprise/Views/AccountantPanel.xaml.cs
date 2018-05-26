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
    /// Interaction logic for AccountantPanel.xaml
    /// </summary>
    public partial class AccountantPanel : UserControl
    {
        public AccountantPanel()
        {
            InitializeComponent();
            DataContext = new AccountantPanelViewModel();
        }
    }
}
