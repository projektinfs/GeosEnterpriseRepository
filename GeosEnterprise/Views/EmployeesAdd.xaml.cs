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
using System.Windows.Shapes;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for EmployeesAdd.xaml
    /// </summary>
    public partial class EmployeesAdd : Window
    {

        int? employeeID { get; set; }

        public EmployeesAdd()
        {
            DataContext = new EmployeesAddViewModel(null);
            InitializeComponent();
        }

        public EmployeesAdd(int employeeID)
        {
            DataContext = new EmployeesAddViewModel(employeeID);
            InitializeComponent();
        }

        public EmployeesAdd(int employeeID, bool IsAdminMode)
        {
            DataContext = new EmployeesAddViewModel(employeeID, IsAdminMode);
            InitializeComponent();
        }

    }
}
