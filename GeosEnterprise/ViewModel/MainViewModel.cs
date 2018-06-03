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

        public ICommand StartPanel { get; set; }
        public ICommand ComputersList { get; set; }
        public ICommand EmployeesList { get; set; }
        public ICommand ClientsList { get; set; }
        public ICommand Logout { get; set; }
        public ICommand AccountantPanel { get; set; }
        public ICommand SchedulerPanel { get; set; }

        public int GlobalPropertyChanged { get; }

        public MainViewModel()
        {
            InitializeDatabase();
            IsAuthenticated = "Hidden";
            StartPanel = new RelayCommand<object>(StartPanelVM);
            ComputersList = new RelayCommand<object>(ComputersListVM);
            EmployeesList = new RelayCommand<object>(EmployeesListVM);
            ClientsList = new RelayCommand<object>(ClientsListVM);
            AccountantPanel = new RelayCommand<object>(AccountantPanelVM);
            Logout = new RelayCommand<object>(LogoutVM);
            SchedulerPanel = new RelayCommand<object>(SchedulerPanelVM);
            Messenger.Default.Register<ViewModelBase>(this, MessageHandler);
            Messenger.Default.Register<String>(this, AuthenticationValid);
            Messenger.Default.Register<String>(this, UserName);
            ViewModel = new AuthenticationViewModel();
            
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

        private void SchedulerPanelVM(object obj)
        {
            ViewModel = new SchedulerPanelViewModel();
        }

        private void LogoutVM(object obj)
        {
            ViewModel = new AuthenticationViewModel();
        }

        private void InitializeDatabase()
        {
            if (!App.DB.Database.Exists())
            {
                App.DB.Database.Create();
                SeedDatabase();
            }
            else if (!App.DB.Database.CompatibleWithModel(false) && Config.DropAndCreateWhenModelChanges)
            {
                App.DB.Database.Delete();
                App.DB.Database.Create();
                SeedDatabase();
            }
            App.DB.Computers.Any();
        }

        private void SeedDatabase()
        {
            foreach (var client in DBO.Client.ForSeedToDatabase())
            {
                Repositories.ClientRepository.Add(client);
            }

            foreach (var employee in DBO.Employee.ForSeedToDatabase())
            {
                Repositories.EmployeeRepository.Add(employee);
            }

            foreach (var repair in DBO.Repair.ForSeedToDatabase())
            {
                Repositories.RepairsRepository.Add(repair);
            }
        }

    }
}
