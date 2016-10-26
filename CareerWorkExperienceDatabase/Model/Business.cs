using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class Business
    {
        public int ID { get; }
        public string Name { get; }
        public string Address { get; }
        public int CityID { get; }
        public string ContactName { get; }
        public string ContactPhone { get; }
        public string ContactEmail { get; }
        public DateTime LastUpdated { get; }
        public string LastUpdatedBy { get; }
        public DateTime Expries { get; }
        public bool RqInterview { get; }
        public bool RqCriminalRecordCheck { get; }
        public string SpecialRequirements { get; }
        public bool NotInterested { get; }
        
    }
}