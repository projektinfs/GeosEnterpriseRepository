using GeosEnterprise.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GeosEnterprise;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace GeosEnterprise.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase _ViewModel;
        public ViewModelBase ViewModel
        {
            get
            {
                return _ViewModel;
            }
            set
            {
                _ViewModel = value;
                RaisePropertyChanged("ViewModel");
            }
        }
        private String _IsAuthenticated;
        public String IsAuthenticated
        {
            get { return _IsAuthenticated; }
            set
            {
                _IsAuthenticated = value;
                RaisePropertyChanged("IsAuthenticated");
            }
        }

        private String _Name;
        public String Name
        {
            get { return _IsAuthenticated; }
            set
            {
                _IsAuthenticated = value;
                RaisePropertyChanged("Name");
            }
        }
        
        public String _RepairSchedulerPermission;
        public String RepairSchedulerPermission
        {
            get
            {
                return _RepairSchedulerPermission;
            }
            set
            {
                _RepairSchedulerPermission = value;
                RaisePropertyChanged("RepairSchedulerPermission");
            }
        }

        public String _ComputerListPermission;
        public String ComputerListPermission
        {
            get
            {
                return _ComputerListPermission;
            }
            set
            {
                _ComputerListPermission = value;
                RaisePropertyChanged("ComputerListPermission");
            }
        }

        public String _EmployeeListPermission;
        public String EmployeeListPermission
        {
            get
            {
                return _EmployeeListPermission;
            }
            set
            {
                _EmployeeListPermission = value;
                RaisePropertyChanged("EmployeeListPermission");
            }
        }

        public String _ClientsListPermission;
        public String ClientsListPermission
        {
            get
            {
                return _ClientsListPermission;
            }
            set
            {
                _ClientsListPermission = value;
                RaisePropertyChanged("ClientsListPermission");
            }
        }

        public String _AccountantPanelPermission;
        public String AccountantPanelPermission
        {
            get
            {
                return _AccountantPanelPermission;
            }
            set
            {
                _AccountantPanelPermission = value;
                RaisePropertyChanged("AccountantPanelPermission");
            }
        }

        public String _EmployeeSchedulerPermission;
        public String EmployeeSchedulerPermission
        {
            get
            {
                return _EmployeeSchedulerPermission;
            }
            set
            {
                _EmployeeSchedulerPermission = value;
                RaisePropertyChanged("EmployeeSchedulerPermission");
            }
        }

        public String _LogsPermission;
        public String LogsPermission
        {
            get
            {
                return _LogsPermission;
            }
            set
            {
                _LogsPermission = value;
                RaisePropertyChanged("LogsPermission");
            }
        }

        public ICommand StartPanel { get; set; }
        public ICommand ComputersList { get; set; }
        public ICommand EmployeesList { get; set; }
        public ICommand ClientsList { get; set; }
        public ICommand Logout { get; set; }
        public ICommand AccountantPanel { get; set; }
        public ICommand EmployeeSchedulerPanel { get; set; }
        public ICommand RepairSchedulerPanel { get; set; }
        public ICommand LogsList { get; set; }

        public int GlobalPropertyChanged { get; }

        public MainViewModel()
        {
            IsAuthenticated = "Hidden";
            StartPanel = new RelayCommand<object>(StartPanelVM);
            ComputersList = new RelayCommand<object>(ComputersListVM);
            EmployeesList = new RelayCommand<object>(EmployeesListVM);
            ClientsList = new RelayCommand<object>(ClientsListVM);
            AccountantPanel = new RelayCommand<object>(AccountantPanelVM);
            Logout = new RelayCommand<object>(LogoutVM);
            EmployeeSchedulerPanel = new RelayCommand<object>(EmployeeSchedulerPanelVM);
            RepairSchedulerPanel = new RelayCommand<object>(RepairSchedulerPanelVM);
            LogsList = new RelayCommand<object>(LogsVM);
            Messenger.Default.Register<ViewModelBase>(this, MessageHandler);
            Messenger.Default.Register<String>(this, AuthenticationValid);
            Messenger.Default.Register<String>(this, UserName);
            Messenger.Default.Register<Dictionary<string, bool>>(this, SetAccess);
            ViewModel = new AuthenticationViewModel();
        }

        private void SetAccess(Dictionary<string, bool> obj)
        {
            if (obj["ComputerList"])
                ComputerListPermission = "Visible";
            else
                ComputerListPermission = "Collapsed";

            if (obj["EmployeeList"])
                EmployeeListPermission = "Visible";
            else
                EmployeeListPermission = "Collapsed";

            if (obj["ClientsList"])
                ClientsListPermission = "Visible";
            else
                ClientsListPermission = "Collapsed";

            if (obj["AccountantPanel"])
                AccountantPanelPermission = "Visible";
            else
                AccountantPanelPermission = "Collapsed";

            if (obj["EmployeeScheduler"])
                EmployeeSchedulerPermission = "Visible";
            else
                EmployeeSchedulerPermission = "Collapsed";

            if (obj["RepairScheduler"])
                RepairSchedulerPermission = "Visible";
            else
                RepairSchedulerPermission = "Collapsed";

            if (obj["Logs"])
                LogsPermission = "Visible";
            else
                LogsPermission = "Collapsed";
        }

        private void UserName(string obj)
        {
            Name = obj;
        }

        private void AuthenticationValid(string obj)
        {
            IsAuthenticated = obj;
        }

        private void MessageHandler(ViewModelBase obj)
        {
            ViewModel = obj;
        }

        private void StartPanelVM(object obj)
        {
            ViewModel = new StartPanelViewModel();
        }

        private void ComputersListVM(object obj)
        {
            ViewModel = new ComputersListViewModel();
        }

        private void EmployeesListVM(object obj)
        {
            ViewModel = new EmployeesListViewModel();
        }

        private void ClientsListVM(object obj)
        {
            ViewModel = new ClientsListViewModel();
        }

        private void AccountantPanelVM(object obj)
        {
            ViewModel = new AccountantPanelViewModel();
        }

        private void EmployeeSchedulerPanelVM(object obj)
        {
            ViewModel = new EmployeeSchedulerViewModel();
        }

        private void RepairSchedulerPanelVM(object obj)
        {
            ViewModel = new RepairSchedulerViewModel();
        }

        private void LogoutVM(object obj)
        {
            ViewModel = new AuthenticationViewModel();
        }

        private void LogsVM(object obj)
        {
            ViewModel = new LogsListViewModel();
        }
    }
}
