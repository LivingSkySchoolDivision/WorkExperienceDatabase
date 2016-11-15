using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class Business
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime Expires { get; set; }
        public bool RqInterview { get; set; }
        public bool RqCriminalRecordCheck { get; set; }
        public string SpecialRequirements { get; set; }
        public bool Interested { get; set; }
        public bool NotInterested { get
            {
                return !Interested;
            }
        }
    }
}