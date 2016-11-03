using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class PositionReview
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public bool StaffReviewer { get; set; }
        public bool Private { get; set; }
    }
}