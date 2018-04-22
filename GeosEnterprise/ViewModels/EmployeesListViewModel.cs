using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using GeosEnterprise.Views;

namespace GeosEnterprise.ViewModels
{
    public class EmployeesListViewModel : PropertyChangedBase
    {
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand EditButtonCommand { get; set; }
        public ICommand InfoButtonCommand { get; set; }



        public object SelectedItem { get; set; }



        private DateTime? timeToBindingItem;
        public DateTime? TimeToBindingItem
        {
            get
            {
                return timeToBindingItem;
            }
            set
            {
                if (timeToBindingItem != value)
                {
                    timeToBindingItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? timeFromBindingItem;
        public DateTime? TimeFromBindingItem
        {
            get
            {
                return timeFromBindingItem;
            }
            set
            {
                if (timeFromBindingItem != value)
                {
                    timeFromBindingItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public EmployeesListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            AddButtonCommand = new RelayCommand<object>(Add);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            EditButtonCommand = new RelayCommand<object>(Edit);
            InfoButtonCommand = new RelayCommand<object>(Info);


        }


        public ObservableCollection<EmployeeDTO> Items
        {
            get
            {
                return new ObservableCollection<EmployeeDTO>(EmployeeRepository.GetAllCurrent().Select(p => DTO.EmployeeDTO.ToDTO(p)));
            }
        }

        public void Now(object obj)
        {
            TimeToBindingItem = DateTime.Now;
        }

        public void Reset(object obj)
        {
            TimeToBindingItem = null;
            TimeFromBindingItem = null;
        }

        private void Add(object obj)
        {
            Window addNewEmployeeWindow = new EmployeesAdd();
            addNewEmployeeWindow.Show();


        }

        private void Delete(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Repositories.EmployeeRepository.Delete(employeeDTO.ID);
            }

        }

        private void Edit(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeesAdd(employeeDTO.ID);
                addNewEmployeeWindow.Show();

            }

        }

        private void Info(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeeInfo(employeeDTO.ID);
                addNewEmployeeWindow.ShowDialog();
            }

        }
    }
}

