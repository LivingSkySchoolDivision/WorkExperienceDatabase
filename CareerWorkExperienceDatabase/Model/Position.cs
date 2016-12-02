using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWorkExperienceDatabase
{
    public class Position
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BusinessID { get; set; }
        public int PositionTypeID { get; set; }
        public bool Seasonal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime Expires { get; set; }
        public string SearchTags { get; set; }
        public List<Category> Categories { get; set; }
        public List<PositionFlag> Flags { get; set; }

        public Position()
        {
            this.Flags = new List<PositionFlag>();
        }

        public List<COPSCategory> COPSCategories { get
            {
                Dictionary<int,COPSCategory> returnMe = new Dictionary<int, COPSCategory>();

                foreach(Category category in this.Categories)
                {
                    foreach(COPSCategory copscat in category.COPSCategories)
                    {
                        if (!returnMe.ContainsKey(copscat.ID))
                        {
                            returnMe.Add(copscat.ID, copscat);
                        }
                    }
                }

                return returnMe.Values.OrderBy(c => c.Number).ToList();
            }
        }
    }
}