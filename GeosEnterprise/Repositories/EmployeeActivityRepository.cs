using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.Repositories
{
    public class EmployeeActivityRepository : BaseRepository<EmployeeActivity>
    {
        public new static EmployeeActivity GetByAll(DateTime TimeFrom, DateTime TimeTo, string Description)
        {
            return GetAllCurrent().Where(p => p.TimeFrom == TimeFrom).
                Where(p => p.TimeTo == TimeTo).
                Where(p => p.Description == Description).FirstOrDefault();
        }

    }
}
