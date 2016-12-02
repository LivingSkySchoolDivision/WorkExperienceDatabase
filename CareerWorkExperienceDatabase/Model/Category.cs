using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<COPSCategory> COPSCategories { get; set; }

        public Category()
        {
            this.COPSCategories = new List<COPSCategory>();
        }
    }
}