using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class ActivePosition
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public string StudentName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}