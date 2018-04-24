﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Repair : DBObject<int>
    {
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual Computer Computer { get; set; }
        public int ComputerID { get; set; }
        public string Description { get; set; }
        public DateTime? RealizationDate { get; set; }

        public string OrderNumber
        {
            get
            {
                return $"{ID}/{CreatedDate.Value.Year}/{CreatedDate.Value.Month}";
            }
        }
    }
}
