using GeosEnterprise.DBO;
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
        int? repairID { get; set; }

        public ComputersAdd(int repairID)
        {
            InitializeComponent();
            this.repairID = repairID;
            Repair repair = Repositories.RepairsRepository.GetById(repairID);
            DescriptionTextBox.Text = repair.Description;
            SerialNrTextBox.Text = repair.Computer.SerialNumber;
        }

        public ComputersAdd()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            if (this.repairID == null)
            {
                Repositories.RepairsRepository.Add(new Repair
                {
                    Description = DescriptionTextBox.Text,
                    Computer = new Computer
                    {
                        SerialNumber = SerialNrTextBox.Text,
                        Components = new List<Component>
                        {
                            new Component()
                            {
                            }
                        },
                    }
                });
            }
            else
            {
                Repositories.RepairsRepository.Edit(new Repair
                {
                    ID = (int)repairID,
                    Description = DescriptionTextBox.Text,
                    Computer = new Computer
                    {
                        SerialNumber = SerialNrTextBox.Text,
                        Components = new List<Component>
                        {
                            new Component()
                            {
                            }
                        },
                    }
                });
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            return;
        }
    }
}
