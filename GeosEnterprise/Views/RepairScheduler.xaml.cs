using GeosEnterprise.DBO;
using GeosEnterprise.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfScheduler;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for RepairScheduler.xaml
    /// </summary>
    public partial class RepairScheduler : UserControl
    {
        public RepairScheduler()
        {
            InitializeComponent();
            DataContext = new RepairSchedulerViewModel();
        }

        public Event Event { get; internal set; }

        private void scheduler_Loaded(object sender, RoutedEventArgs e)
        {
            scheduler.SelectedDate = DateTime.Now;
            scheduler.Mode = Mode.Day;
            scheduler.StartJourney = new TimeSpan(7, 0, 0);
            scheduler.EndJourney = new TimeSpan(22, 0, 0);
            scheduler.Loaded += scheduler_Loaded;

            List<Repair> repairs = getRepairs();

            foreach (Repair repair in repairs)
            {
                if( repair.Serviceman != null || repair.Computer != null || repair.ServicemanID != null)
                {
                    Event = new Event()
                    {
                        Subject = repair.Serviceman.Name + " " + repair.Serviceman.Surname + " "
                    + Environment.NewLine + repair.Computer.Name + Environment.NewLine +
                    repair.Computer.SerialNumber + " " + Environment.NewLine + repair.ID,
                        Color = Brushes.Aqua,
                        Start = (DateTime)repair.CreatedDate,
                        End = (DateTime)repair.EstimatedDate
                    };

                    scheduler.Events.Add(Event);
                }
            }

        }

        void scheduler_OnScheduleDoubleClick(object sender, DateTime e)
        {
            
        }

        void scheduler_OnEventDoubleClick(object sender, Event e)
        {
            var names = e.Subject.Split(' ');
            var m = int.Parse(Regex.Match(names[3], @"[0-9]+").ToString());

            Window addRepairInfoWindow = new ComputersInfo(m);

            if (addRepairInfoWindow.ShowDialog() == true)
            {
                MessageBox.Show("Edytowano zlecenie!");
            }

        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler.PrevPage();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler.NextPage();
        }

        private List<Repair> getRepairs()
        {
            return new List<Repair>(Repositories.RepairsRepository.GetAllWithEstimatedDate());
        }

        private List<Repair> getRepairByID()
        {
            return new List<Repair>(Repositories.RepairsRepository.GetAllWithEstimatedDate());
        }
    }
}
