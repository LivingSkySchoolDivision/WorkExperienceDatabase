using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CareerWorkExperienceDatabase
{
    public partial class viewPosition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) == false)
            {
                string idString = Request.QueryString["id"];
                int id = Parsers.ParseInt(idString);

                if (id > 0)
                {
                    PositionRepository positionRepo = new PositionRepository();
                    Position position = positionRepo.Get(id);

                    if (position != null)
                    {
                        displayPosition(position);

                        BusinessRepository businessRepo = new BusinessRepository();
                        Business business = businessRepo.Get(position.BusinessID);
                        if (business != null)
                        {
                            displayBusiness(business);
                        }                       

                    }

                }
            }
            
        }

        private void displayPosition(Position position)
        {
            // Figure out position type
            PositionTypeRepository positionTypeRepo = new PositionTypeRepository();
            PositionType positionType = positionTypeRepo.Get(position.PositionTypeID);

            // Figure out business type
            BusinessRepository businessRepo = new BusinessRepository();
            Business business = businessRepo.Get(position.BusinessID);

            // Figure out location type
            CityRepository cityRepo = new CityRepository();
            City city = cityRepo.Get(business.CityID);

            // Display flags
            litPositionFlags.Text = "<div class=\"position_flags\">";
            foreach (PositionFlag flag in position.Flags)
            {
                litPositionFlags.Text += "<img class=\"position_flag_icon\" src=\"/Images/PositionFlags/" + flag.Icon + "\" alt=\"" + flag.Name + "\" title=\"" + flag.Name + "\n" + flag.Description + "\">";
            }
            litPositionFlags.Text += "</div>";

            // Display COPS badges
            if (position.COPSCategories.Count > 0)
            {
                litCOPSBadges.Text = "<div class=\"COPS_badges\">";
                litCOPSBadges.Text = "<div class=\"mainPageTitle\"><a href=\"WhatAreCOPSCategories.aspx\">COPS Categories</a></div>";
                foreach (COPSCategory copscat in position.COPSCategories)
                {
                    litCOPSBadges.Text += "<img class=\"cops_badge\" src=\"/Images/COPSBadges/" + copscat.Icon + "\" ALT=\"\" TITLE=\"" + copscat.Number + ": " + copscat.Name + "\n" + copscat.Description + "\">";
                }
                litCOPSBadges.Text += "</div>";
            }

            lblPositionName.Text = position.Name;
            litPositionDescription.Text = "<div class=\"position_description\">" + position.Description + "</div>";

            TableRow positionTypeRow = new TableRow();
            positionTypeRow.Cells.Add(new TableCell() { Text = "Position Type" });
            positionTypeRow.Cells.Add(new TableCell() { Text = positionType.Name });
            tblPositionDetails.Rows.Add(positionTypeRow);

            TableRow locationRow = new TableRow();
            locationRow.Cells.Add(new TableCell() { Text = "Location" });
            locationRow.Cells.Add(new TableCell() { Text = city.Name });
            tblPositionDetails.Rows.Add(locationRow);

            TableRow startDateRow = new TableRow();
            startDateRow.Cells.Add(new TableCell() { Text = "Start Date" });
            startDateRow.Cells.Add(new TableCell() { Text = position.StartDate.ToShortDateString() });
            tblPositionDetails.Rows.Add(startDateRow);

            TableRow endDateRow = new TableRow();
            endDateRow.Cells.Add(new TableCell() { Text = "End Date" });
            endDateRow.Cells.Add(new TableCell() { Text = position.EndDate.ToShortDateString() });
            tblPositionDetails.Rows.Add(endDateRow);

            TableRow lastUpdatedRow = new TableRow();
            lastUpdatedRow.Cells.Add(new TableCell() { Text = "Last Updated On" });
            lastUpdatedRow.Cells.Add(new TableCell() { Text = position.LastUpdated.ToShortDateString() });
            tblPositionDetails.Rows.Add(lastUpdatedRow);

            // Build list of categories
            string categories = "None";
            if (position.Categories.Count > 0)
            {
                StringBuilder categoryBuilder = new StringBuilder();
                foreach(Category cat in position.Categories)
                {
                    categoryBuilder.Append("<a href=\"/byCategory.aspx?id=" + cat.ID + "\">");
                    categoryBuilder.Append(cat.Name);
                    categoryBuilder.Append("</a>");
                    categoryBuilder.Append(", ");
                }

                // remove the final ", " from the end of the string
                categoryBuilder.Remove(categoryBuilder.Length - 2, 2);

                categories = categoryBuilder.ToString();
            }
            

            TableRow categoriesRow = new TableRow();
            categoriesRow.Cells.Add(new TableCell() { Text = "Categories" });
            categoriesRow.Cells.Add(new TableCell() { Text = categories });
            tblPositionDetails.Rows.Add(categoriesRow);


            string isSeasonal;
            if (position.Seasonal == true)
            {
                isSeasonal = "Yes";
            }else
            {
                isSeasonal = "No";
            }

            TableRow seasonalRow = new TableRow();
            seasonalRow.Cells.Add(new TableCell() { Text = "Seasonal" });
            seasonalRow.Cells.Add(new TableCell() { Text = isSeasonal });
            tblPositionDetails.Rows.Add(seasonalRow);


        }

        private void displayBusiness(Business business)
        {
            lblBusinessName.Text = business.Name;
        }

    }
}