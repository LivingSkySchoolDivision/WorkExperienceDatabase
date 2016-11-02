using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class Position
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public int BusinessID { get; }
        public int PositionTypeID { get; }
        public bool Seasonal { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateTime LastUpdated { get; }
        public string LastUpdatedBy { get; }
        public DateTime Expires { get; }
        public string SearchTags { get; }
    }
}