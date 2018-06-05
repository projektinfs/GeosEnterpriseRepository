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
using WpfScheduler;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        public NewEvent()
        {
            InitializeComponent();
        }

        public Event Event { get; internal set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Event = new Event()
            {
                Subject = name.Text,
                Color = Brushes.Green,
                Start = (DateTime)from.Value,
                End = (DateTime)to.Value
            };

            this.Close();
        }
    }
}
