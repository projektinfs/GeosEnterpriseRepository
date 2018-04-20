using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeosEnterprise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EntitiesContext DB = new EntitiesContext();
    }

    public class Session
    {
        public static string Username = "admin";
    }
}
