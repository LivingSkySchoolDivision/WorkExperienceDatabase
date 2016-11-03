using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase.Model
{
    public class PositionNote
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public string Note { get; set; }
    }
}