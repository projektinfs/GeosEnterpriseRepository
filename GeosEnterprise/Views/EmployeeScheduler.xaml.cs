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
    /// Interaction logic for EmployeeSchedulerPanel.xaml
    /// </summary>
    public partial class EmployeeSchedulerPanel : UserControl
    {
        public EmployeeSchedulerPanel()
        {
            InitializeComponent();
            DataContext = new EmployeeSchedulerViewModel();
        }

        public Event Event { get; internal set; }

        private void scheduler_Loaded(object sender, RoutedEventArgs e)
        {
            scheduler.SelectedDate = DateTime.Now;
            scheduler.Mode = Mode.Day;
            scheduler.StartJourney = new TimeSpan(7, 0, 0);
            scheduler.EndJourney = new TimeSpan(22, 0, 0);
            scheduler.Loaded += scheduler_Loaded;

            List<EmployeeActivity> activities = getActivities();

            foreach (EmployeeActivity activity in activities)
            {
                string description = returnType(activity.Description);

                SolidColorBrush color;

                if (description == "Pracuje")
                    color = Brushes.Green;
                else if (description == "Wolne")
                    color = Brushes.Red;
                else if (description == "L4")
                    color = Brushes.Orange;
                else
                    color = Brushes.Blue;

                Employee employee = Repositories.EmployeeRepository.GetById(activity.EmployeeID);

                Event = new Event()
                {
                        Subject = activity.Description,
                        Color = color,
                        Start = (DateTime)activity.TimeFrom,
                        End = (DateTime)activity.TimeTo
                };

                scheduler.Events.Add(Event);
            }
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
                    
                    EmployeeActivity activity = Repositories.EmployeeActivityRepository.GetByAll(e.Start, e.End, e.Subject);

                    if (activity != null)
                    {
                        Repositories.EmployeeActivityRepository.Delete(activity);
                    }
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

        private List<EmployeeActivity> getActivities()
        {
            return new List<EmployeeActivity>(Repositories.EmployeeActivityRepository.GetAllCurrent());
            
            /*
            if (Authorization.AcctualEmployee.UserRole == UserRole.Administrator || Authorization.AcctualEmployee.UserRole == UserRole.Manager)
            {
                return new List<EmployeeActivity>(Repositories.EmployeeActivityRepository.GetAllCurrent());
            }
            else
            {
                return new List<EmployeeActivity>(Repositories.EmployeeActivityRepository.GetAllCurrent().Where(p => p.ID == Authorization.AcctualEmployee.ID));
            }
            */
        }
        
        public string returnType(string Description)
        {
            var names = Description.Split(' ');
            return names[2];
        }
    }
}
