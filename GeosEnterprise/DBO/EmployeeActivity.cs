using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class EmployeeActivity : DBObject<int>
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Description { get; set; }
        public int EmployeeID { get; set; }

        public static IList<EmployeeActivity> ForSeedToDatabase()
        {
            return new List<EmployeeActivity>
            {
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-11 08:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-11 15:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Admin Adminowski Pracuje",
                    EmployeeID = 1
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-12 09:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-12 17:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Admin Adminowski Pracuje",
                    EmployeeID = 1
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-13 07:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-13 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Admin Adminowski Pracuje",
                    EmployeeID = 1
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-11 07:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-11 15:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Andrzej Kasztan Pracuje",
                    EmployeeID = 2
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-20", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-27", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Admin Adminowski Wolne",
                    EmployeeID = 1
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-15", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-06-17", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Andrzej Kasztan Wolne",
                    EmployeeID = 2
                },
                new EmployeeActivity
                {
                    TimeFrom = DateTime.ParseExact("2018-06-11", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TimeTo = DateTime.ParseExact("2018-07-02", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "Katarzyna Karmowska L4",
                    EmployeeID = 3
                },

            };
        }

    }
}
