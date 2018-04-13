using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public enum ComponentType
    {
        MotherBoard = 1,
        CPU = 2,
        GPU = 3,
        RAM = 4,
        HDD = 5,
        Monitor = 6,
        Case = 7,
        Speakers = 8,
        Unknown = 99
    }

    public enum Actors
    {
        Serviceman = 1,
        Salesman = 2,
        Manager = 3,
        Accountant = 4
    }
}
