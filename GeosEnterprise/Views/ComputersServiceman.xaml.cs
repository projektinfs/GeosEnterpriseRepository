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
using System.Windows.Shapes;

namespace GeosEnterprise.Views
{
 
    public partial class ComputersServiceman : Window
    {
        public ComputersServiceman(int repairID)
        {
            DataContext = new ComputersServicemanViewModel(repairID);
            InitializeComponent();
        }
    }
}
