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
using GeosEnterprise.ViewModels;

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
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Repositories.RepairsRepository.Add(new Repair
            {
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin",
                ID = 1,
                Description = "Opis",
                Computer = new Computer
                {
                    ID = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    SerialNumber = "xxx",
                    Components = new List<Component>
                        {
                            new Component()
                            {
                                ID = 1
                            }
                        },
                }
            });
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Repositories.RepairsRepository.Delete((Repair)RepairsList.SelectedItem);
        }
    }
}
