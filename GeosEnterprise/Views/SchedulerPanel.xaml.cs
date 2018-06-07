using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
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
using WpfScheduler;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for SchedulerPanel.xaml
    /// </summary>
    public partial class SchedulerPanel : UserControl
    {
        public SchedulerPanel()
        {
            InitializeComponent();
            DataContext = new SchedulerPanelViewModel();
        }

        public Event Event { get; internal set; }

        private void scheduler_Loaded(object sender, RoutedEventArgs e)
        {
            scheduler.SelectedDate = DateTime.Now;
            scheduler.Mode = Mode.Day;
            scheduler.StartJourney = new TimeSpan(7, 0, 0);
            scheduler.EndJourney = new TimeSpan(22, 0, 0);
            scheduler.Loaded += scheduler_Loaded;

            /*
            if(Authorization.AcctualEmployee.UserRole == UserRole.Serviceman || Authorization.AcctualEmployee.UserRole == UserRole.Administrator)
            {
                List<RepairDTO> repairs = getDataSource();

                foreach (RepairDTO repair in repairs)
                {
                    if (repair.RealizationDate != null && repair.CreatedDate != null && repair.Status == DBO.RepairStatus.Completed)
                    {
                        Event = new Event()
                        {
                            Subject = "NARPAWA: " + repair.Computer.Name + " " + repair.Computer.SerialNumber,
                            Color = Brushes.Orange,
                            Start = (DateTime)repair.CreatedDate,
                            End = (DateTime)repair.RealizationDate
                        };

                        scheduler.Events.Add(Event);
                    }
                }
            }
            */
        }

        void scheduler_OnScheduleDoubleClick(object sender, DateTime e)
        {
            Console.WriteLine(e.ToShortDateString() + ((FrameworkElement)sender).Name);
            NewEvent window = new NewEvent();
            window.ShowDialog();

            if (window.Event != null)
                scheduler.Events.Add(window.Event);

            scheduler.InvalidateVisual();
        }

        void scheduler_OnEventDoubleClick(object sender, Event e)
        {
            if (e.Subject != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć wydarzenie: {e.Subject}",
                    "Usunięcie wydarzenia", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    scheduler.Events.Remove(e);
                }
            }
            else
                Config.MsgBoxNothingSelectedMessage();

            scheduler.InvalidateVisual();
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler.PrevPage();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler.NextPage();
        }

        /*
        private List<RepairDTO> getDataSource()
        {

            return new List<RepairDTO>(Repositories.RepairsRepository
            .GetAllCompleted()
            .Where(p => p.Status == DBO.RepairStatus.Completed)
            .Select(p => DTO.RepairDTO.ToDTO(p)));
        }
        */
        
    }
}
