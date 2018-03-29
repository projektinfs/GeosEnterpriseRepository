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

namespace GeosEnterprise
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
            InitializeMenu();
        }

        public void InitializeMenu()
        {
            // Tu dodajemy kolejne pozycje do menu: wystarczy przekopiować całą linijkę 
            // i zmienić nazwę, a zawsze fajnie się ponumerują na liście
            MainMenu.Items.Add($"{MainMenu.Items.Count + 1}. Zgłoszone komputery");
            
        }
    }
}
