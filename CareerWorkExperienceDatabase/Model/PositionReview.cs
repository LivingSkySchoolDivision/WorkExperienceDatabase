using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase.Model
{
    public class PositionReview
    {
        public int ID { get; }
        public int PoitionID { get; }
        public string Username { get; }
        public string Comment { get; }
        public int Score { get; }
        public bool StaffReviewer { get; }
        public bool Private { get; }
    }
}