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
    /// <summary>
    /// Interaction logic for ComputersAdd.xaml
    /// </summary>
    public partial class ComputersAdd : Window
    {
        public ComputersAdd(int repairID)
        {
            DataContext = new ComputersAddViewModel(repairID);
            InitializeComponent();
        }

        public ComputersAdd()
        {
            DataContext = new ComputersAddViewModel(null);
            InitializeComponent();
        }
    }
}