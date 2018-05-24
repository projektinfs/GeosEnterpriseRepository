using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeosEnterprise.ViewModels
{
    public class MainWindowViewModel
    {

        public ICommand StartPanelCommand { get; private set; }
        public ICommand ComputerListCommand { get; private set; }

     
    }
}
