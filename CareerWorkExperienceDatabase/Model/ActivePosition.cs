using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class ActivePosition
    {
        public int ID { get; }
        public int PositionID { get; }
        public string StudentName { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
    }
}