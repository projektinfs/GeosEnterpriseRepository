using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public enum UserRole
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("Kierownik")]
        Manager = 2,
        [Description("Sprzedawca")]
        Dealer = 3,
        [Description("Serwisant")]
        Serviceman = 4,
        [Description("Księgowy")]
        Accountant = 5,
        [Description("Nieznany")]
        Unknown = 50
    }

    public enum Actors
    {
        Serviceman = 1,
        Salesman = 2,
        Manager = 3,
        Accountant = 4
    }
}
