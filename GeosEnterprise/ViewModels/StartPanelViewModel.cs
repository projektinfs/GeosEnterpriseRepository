using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeosEnterprise.ViewModels
{
    public class StartPanelViewModel
    {
        public StartPanelViewModel()
        {
            MessageBox.Show(Authorization.AcctualUser);
        }

    }
}
