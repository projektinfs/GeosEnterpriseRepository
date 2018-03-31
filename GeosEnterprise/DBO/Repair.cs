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
        public Client Client { get; set; }
        public string Description { get; set; }
        public DateTime? RealizationDate { get; set; }
    }
}
