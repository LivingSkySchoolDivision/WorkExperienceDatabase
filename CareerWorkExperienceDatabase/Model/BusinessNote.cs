using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class BusinessNote
    {
        public int ID { get; set; }
        public int BusinessID { get; set; }
        public string Notes { get; set; }
        public bool Private { get; set; }
    }
}