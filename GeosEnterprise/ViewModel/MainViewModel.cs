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
        public ViewModelBase ViewModel { get; set; }
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

        public ICommand StartPanel { get; set; }
        public ICommand ComputersList { get; set; }
        public ICommand EmployeesList { get; set; }
        public ICommand ClientsList { get; set; }
        public ICommand Logout { get; set; }
        public int GlobalPropertyChanged { get; }

        public MainViewModel()
        {
            IsAuthenticated = "Hidden";

            StartPanel = new RelayCommand<object>(StartPanelVM);
            ComputersList = new RelayCommand<object>(ComputersListVM);
            EmployeesList = new RelayCommand<object>(EmployeesListVM);
            ClientsList = new RelayCommand<object>(ClientsListVM);
            Logout = new RelayCommand<object>(LogoutVM);
            Messenger.Default.Register<ViewModelBase>(this, MessageHandler);
            Messenger.Default.Register<String>(this, AuthenticationValid);
            ViewModel = new AuthenticationViewModel();
        }

        private void AuthenticationValid(string obj)
        {
            IsAuthenticated = obj;
            RaisePropertyChanged("IsAuthenticated");
        }

        private void MessageHandler(ViewModelBase obj)
        {
            ViewModel = obj;
            RaisePropertyChanged("ViewModel");
        }

        private void StartPanelVM(object obj)
        {
            ViewModel = new StartPanelViewModel();
            RaisePropertyChanged("ViewModel");
        }

        private void ComputersListVM(object obj)
        {
            ViewModel = new ComputersListViewModel();
            RaisePropertyChanged("ViewModel");
        }

        private void EmployeesListVM(object obj)
        {
            ViewModel = new EmployeesListViewModel();
            RaisePropertyChanged("ViewModel");
        }

        private void ClientsListVM(object obj)
        {
            ViewModel = new ClientsListViewModel();
            RaisePropertyChanged("ViewModel");
        }

        private void LogoutVM(object obj)
        {
            ViewModel = new AuthenticationViewModel();
            RaisePropertyChanged("ViewModel");
        }

    }
}
