using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GeosEnterprise.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelBase StartPanel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartPanelViewModel>();
            }
        }

        public ViewModelBase Authentication
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthenticationViewModel>();
            }
        }

        public ViewModelBase Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartPanelViewModel>();
            SimpleIoc.Default.Register<AuthenticationViewModel>();
            SimpleIoc.Default.Register<ComputersListViewModel>();
            SimpleIoc.Default.Register<ClientsListViewModel>();
            SimpleIoc.Default.Register<AccountantPanelViewModel>();
            SimpleIoc.Default.Register<LogsListViewModel>();
        }
    }
}
