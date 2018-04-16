using GeosEnterprise.Repositories;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
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
using Xceed.Wpf.Toolkit;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : Window
    {
        public TimePicker()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            if ( TimeFrom.Text != null && TimeTo.Text != null)
            {
                IList<Repair> Items = RepairsRepository.GetByTime(TimeFrom.Value, TimeTo.Value);
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            return;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            TimeFrom.Text = null;
            TimeTo.Text = null;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimeTo.Value = DateTime.Now;
        }
    }
}
