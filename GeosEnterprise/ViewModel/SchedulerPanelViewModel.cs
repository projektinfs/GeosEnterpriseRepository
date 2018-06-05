using GalaSoft.MvvmLight;
using GeosEnterprise.Commands;
using GeosEnterprise.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfScheduler;

namespace GeosEnterprise.ViewModel
{
    public class SchedulerPanelViewModel : ViewModelBase
    {
        public ICommand DayButtonCommand { get; set; }
        public ICommand WeekButtonCommand { get; set; }
        public ICommand MonthButtonCommand { get; set; }

        public ICommand PrevButtonCommand { get; set; }
        public ICommand NextButtonCommand { get; set; }

        public SchedulerPanelViewModel()
        {
            DayButtonCommand = new RelayCommand<object>(Day);
            WeekButtonCommand = new RelayCommand<object>(Week);
            MonthButtonCommand = new RelayCommand<object>(Month);

            PrevButtonCommand = new RelayCommand<object>(Prev);
            NextButtonCommand = new RelayCommand<object>(Next);
        }

        private Mode mode;
        public Mode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                if (mode != value)
                {
                    mode = value;
                    RaisePropertyChanged("Mode");
                }
            }
        }

        public void Day(object obj)
        {
            Mode = Mode.Day;
            RaisePropertyChanged("Mode");
        }

        public void Week(object obj)
        {
            Mode = Mode.Week;
            RaisePropertyChanged("Mode");
        }

        public void Month(object obj)
        {
            Mode = Mode.Month;
            RaisePropertyChanged("Mode");
        }

        public void Prev(object obj)
        {

        }

        public void Next(object obj)
        {

        }
    }
}
