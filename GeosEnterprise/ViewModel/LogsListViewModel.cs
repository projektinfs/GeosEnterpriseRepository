using GalaSoft.MvvmLight;
using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GeosEnterprise.ViewModel
{
    public class LogsListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public LogsListViewModel()
        {
            _myDataSource = new ObservableCollection<Log>(Repositories.LogsRepository.GetAllCurrent());
        }

        public object SelectedItem { get; set; }

        private ICollectionView _items;
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_myDataSource); }
        }

        public ObservableCollection<Log> _myDataSource = new ObservableCollection<Log>();




    }
}
