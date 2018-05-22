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
        [Description("Administrator")] // do wywalenia
        Admin = 0,
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

    public enum RepairStatus
    {
        [Description("Usterka zgłoszona")]
        Reported = 1,
        [Description("W trakcie naprawy")]
        InProcess = 2,
        [Description("Naprawa zakończona")]
        Completed = 3,
        [Description("Odebrana przez klienta")]
        AcceptedByClient = 4
    }
}
